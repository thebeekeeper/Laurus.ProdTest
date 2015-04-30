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
using System.ComponentModel;
using System.Timers;

namespace Laurus.ProdTest.Ui
{
    public class TestStatisticsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TestStatisticsModel()
        {
            _pastRunTimes = new List<TimeSpan>();
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                _serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        public TimeSpan TestTime
        {
            get { return _testTime; }
            set
            {
                _testTime = value;
                OnPropertyChanged("TestTime");
            }
        }

        public TimeSpan AverageTestTime
        {
            get { return _averageTestTime; }
            set
            {
                _averageTestTime = value;
                OnPropertyChanged("AverageTestTime");
            }
        }

        public int UnitsTested
        {
            get { return _unitsTested; }
            set
            {
                _unitsTested = value;
                OnPropertyChanged("UnitsTested");
            }
        }

        public double PassRate
        {
            get { return _passRate; }
            set
            {
                _passRate = value;
                OnPropertyChanged("PassRate");
            }
        }

        public string UserData
		{
			get { return _userData; }
            set
			{
				_userData = value;
				OnPropertyChanged("UserData");
			}
		}

        public void StartTestTimer()
        {
            TestTime = new TimeSpan();
            _timer = new Timer(100);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TestTime = TestTime.Add(new TimeSpan(0, 0, 0, 0, (int)_timer.Interval));
        }

        public void StopTestTimer()
        {
            _timer.Stop();
        }

        public void UpdateAverageTime()
        {
            _pastRunTimes.Add(TestTime);
            var avg = _pastRunTimes.Average(t => t.Ticks);
            AverageTestTime = new TimeSpan((long)avg);
        }

        public void UpdatePassRate(bool passed)
        {
            if(passed)
            {
                _unitsPassed++;
            }
            PassRate = (double)_unitsPassed / (double)_unitsTested;
        }

        public void Reset()
		{
			SerialNumber = string.Empty;
			TestTime = new TimeSpan();
			UserData = string.Empty;
		}

        private string _serialNumber;
        private TimeSpan _testTime;
        private Timer _timer;
        private int _unitsTested;
        private TimeSpan _averageTestTime;
        private IList<TimeSpan> _pastRunTimes;
        private double _passRate;
        private int _unitsPassed;
		private string _userData;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
