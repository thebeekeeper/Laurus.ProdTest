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
using System.Windows.Input;
using Laurus.ProdTest.Core;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laurus.ProdTest.Ui
{
	public class TestRunViewModel : INotifyPropertyChanged
	{
		public ICommand StartCommand { get; set; }

		public ObservableCollection<TestStepModel> TestSteps { get; set; }

		public TestStatisticsModel Statistics { get; set; }

		public string ButtonText
		{
			get { return this._buttonText; }
			set
			{
				if (value != this._buttonText)
				{
					this._buttonText = value;
					NotifyPropertyChanged();
				}
			}
		}

        public string TestComment
		{
			get { return _testComment; }
            set
			{
                if(value != this._testComment)
				{
					this._testComment = value;
					NotifyPropertyChanged();
				}
			}
		}

		public TestRunViewModel()
		{
			TestSteps = new ObservableCollection<TestStepModel>();
			StartCommand = new DelegateCommand(new Action<object>(ProcessTestRunState), (a) => true);
			_execution = TestRunFactory.BuildTestRun();
			_execution.OnTestStepCompleted += new OnTestStepCompletedHandler(_execution_OnTestStepCompleted);
			_execution.OnTestStepStarted += new OnTestStepStartedHandler(_execution_OnTestStepStarted);
			_execution.OnConnected += new OnConnectedHandler(_execution_OnConnected);
			_execution.OnRunCompleted += new OnTestRunCompletedHandler(_execution_OnRunCompleted);
			Statistics = new TestStatisticsModel();
			ResetTest();
			Statistics.UserData = "";
			_currentState = TestExecutionState.IDLE;
			ButtonText = "START";
		}

		private void ResetTest()
		{
			TestComment = string.Empty;
			TestSteps.Clear();
            foreach(var ts in Map(_execution.Steps))
			{
				TestSteps.Add(ts);
			}
		}

		void _execution_OnRunCompleted(object sender, TestRunCompletedEventArgs args)
		{
			Statistics.StopTestTimer();
			Statistics.UpdateAverageTime();
			Statistics.UnitsTested++;
			Statistics.UpdatePassRate(!TestSteps.Where(t => t.StepStatus == TestStepStatus.FAILED).Any());
			Statistics.SerialNumber = args.SerialNumber;
			Statistics.UserData = args.UnitData;
            _currentState = TestExecutionState.ENDED;
			ButtonText = "RESET";
		}

		void _execution_OnConnected(object sender, ConnectedEventArgs args)
		{
			Statistics.SerialNumber = args.Id;
		}

		void _execution_OnTestStepStarted(object sender, TestStepStartedEventArgs args)
		{
			var currentStep = this.TestSteps.Where(s => s.Order == args.Id).First();
			currentStep.StepStatus = TestStepStatus.RUNNING;
		}

		void _execution_OnTestStepCompleted(object sender, TestStepCompletedEventArgs args)
		{
			var currentStep = this.TestSteps.Where(s => s.Order == args.StepResult.TestId).First();
			currentStep.StepStatus = args.StepResult.Passed ? TestStepStatus.PASSED : TestStepStatus.FAILED;
		}

		public void ProcessTestRunState(object param)
		{
            switch(this._currentState)
			{
                case TestExecutionState.IDLE:
        			var firstStep = this.TestSteps.FirstOrDefault();
        			if (firstStep != default(TestStepModel))
		        	{
						_currentState = TestExecutionState.RUNNING;
						ButtonText = "STOP";
						Statistics.StartTestTimer();
						this.TestSteps.First().StepStatus = TestStepStatus.RUNNING;
						var threadStart = new ThreadStart(() => _execution.Execute());
						var t = new Thread(threadStart);
						t.SetApartmentState(ApartmentState.STA);
						t.Start();
					}
					break;
				case TestExecutionState.RUNNING:
                    // on press when running, reset test and don't save any data
					ButtonText = "RESET";
					break;
				case TestExecutionState.ENDED:
					ButtonText = "START";
			        _currentState = TestExecutionState.IDLE;
					_execution.AddComment(TestComment);
					ResetTest();
					Statistics.Reset();
					break;
				default:
					throw new Exception(string.Format("This should be impossible - entered unknown state: {0}", _currentState.ToString()));
			}
		}

		private ObservableCollection<TestStepModel> Map(IList<ITestStep> steps)
		{
			var rval = from s in steps
					   select new TestStepModel()
					   {
						   Order = s.Order,
						   Name = s.Name,
						   StepStatus = TestStepStatus.NOTRUN,
						   Status = "NOT RUN"
					   };
			return new ObservableCollection<TestStepModel>(rval);
		}

		private ITestExecution _execution;

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private TestExecutionState _currentState;
		private string _buttonText;
		private string _testComment;
	}

	public enum TestExecutionState
	{
		IDLE,
		RUNNING,
		ENDED
	};
}
