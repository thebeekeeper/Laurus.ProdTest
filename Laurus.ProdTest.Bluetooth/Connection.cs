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
using System.IO;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;

namespace Laurus.ProdTest.Core
{
    public class Connection
    {
        public BluetoothDeviceInfo Device { get; set; }

        public void Connect(BluetoothDeviceInfo device)
        {
            var address = device.DeviceAddress;
            var serviceClass = BluetoothService.SerialPort;

            var endPoint = new BluetoothEndPoint(address, serviceClass);
            _client = new BluetoothClient();
            _client.Authenticate = true;
            _client.SetPin("0000");
            try
            {
                _client.Connect(endPoint);
            }
            catch (Exception e)
            {
                _client.Close();
                _client.Dispose();
                throw new Exception("Failed to connect socket: " + e);
            }
            _stream = _client.GetStream();

            Device = device;
        }

        /// <summary>
        /// Sends the given command and reads the response
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
		//public IEnumerable<byte> SendCommand(Laurus.ProdTest.Core.ProtocolSchema.Commands command)
		//{
		//	var packet = BuildPacket((int)command);
		//	this.Write(packet);
		//	var response = this.Read();
		//	return response;
		//}

        private byte[] BuildPacket(int command)
        {
            return new byte[] { (byte)command };
        }

        public IEnumerable<byte> Read()
        {
            byte[] buf = new byte[100];
            int length = _stream.Read(buf, 0, buf.Length);
            return buf.Take(length);
        }

        public void Write(byte[] data)
        {
            _stream.Write(data, 0, data.Length);
            _stream.Flush();
        }

        public void Disconnect()
        {
            if (_client.Connected)
            {
                _client.Close();
                _client.Dispose();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _client.Connected;
            }
        }

        private BluetoothClient _client;
        private Stream _stream;
    }
}
