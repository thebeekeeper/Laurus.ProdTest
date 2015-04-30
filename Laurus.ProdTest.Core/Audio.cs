/*
The MIT License (MIT)

Copyright (c) 2013 Nick Gamroth

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.Diagnostics;

namespace Laurus.ProdTest.Core
{
    public class Audio
    {
        /// <summary>
        /// Record audio
        /// </summary>
        /// <param name="time">time to record in msec</param>
        public void Record(int time)
        {
            _waveIn = new WaveInEvent();
            _waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            _waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);
            _waveIn.StartRecording();
            System.Threading.Thread.Sleep(time);
            _waveIn.StopRecording();
            while (!_writeComplete)
                System.Threading.Thread.Sleep(100);
            _waveIn.Dispose();
            _waveIn = null;
        }

        void waveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            _writeComplete = false;

            Debug.WriteLine("recording stopped");
            if (e.Exception != null)
                throw new Exception("recording failed " + e.Exception.Message);

            WaveFileWriter writer = new WaveFileWriter(_outputFile, _waveIn.WaveFormat);
            var bytes = _recordedBytes.ToArray();
            writer.Write(bytes, 0, bytes.Length);
            writer.Flush();
            writer.Close();
            writer.Dispose();

            _writeComplete = true;
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            Debug.WriteLine("read " + e.BytesRecorded + " bytes");
            _recordedBytes = _recordedBytes.Concat(e.Buffer);
        }

        public Audio(string outputFile)
        {
            _recordedBytes = new List<byte>();
            _outputFile = outputFile;
        }

        private IEnumerable<byte> _recordedBytes;
        private WaveInEvent _waveIn;
        private string _outputFile;
        private volatile bool _writeComplete = false;
    }
}
