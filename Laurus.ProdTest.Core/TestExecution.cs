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
using System.Configuration;
using System.Reflection;
using NLog;

namespace Laurus.ProdTest.Core
{
	public class TestExecution : ITestExecution
	{
		public IList<ITestStep> Steps { get; set; }

		public event OnTestStepCompletedHandler OnTestStepCompleted;
		public event OnTestStepStartedHandler OnTestStepStarted;
		public event OnConnectedHandler OnConnected;
		public event OnTestRunCompletedHandler OnRunCompleted;

		void ITestExecution.Execute()
		{
			_log.Debug("Starting test run");

			_record = new TestEntity()
			{
				TestStarted = DateTime.Now
			};

			var context = TestRunFactory.BuildRunContext();
			_log.Info("Initialized test run context.  Starting test steps");
			if (OnConnected != null)
			{
				OnConnected(this, new ConnectedEventArgs() { Id = _record.SerialIdentifier });
			}

			var serialNumber = string.Empty;
			var unitData = string.Empty;
			foreach (var step in Steps)
			{
				if (OnTestStepStarted != null)
				{
					OnTestStepStarted(this, new TestStepStartedEventArgs() { Id = step.Order });
				}
				var result = step.Execute(context);
				result.ParentTestId = _record.Id;
				_record.StepResults.Add(result);
				_log.Info("Step " + step.Order + " completed with result " + result.Passed);
				// TODO: is this supposed to be happening?
				result.TestId = step.Order;
				if (OnTestStepCompleted != null)
				{
					OnTestStepCompleted(this, new TestStepCompletedEventArgs() { StepResult = result });
				}
				if (String.IsNullOrEmpty(result.SerialNumber) == false)
					serialNumber = result.SerialNumber;
				if (string.IsNullOrEmpty(result.UnitData) == false)
					unitData = result.UnitData;
				if (string.IsNullOrEmpty(result.UserData1) == false)
					_record.UserData1 = result.UserData1;
				if (string.IsNullOrEmpty(result.UserData2) == false)
					_record.UserData2 = result.UserData2;
				if (string.IsNullOrEmpty(result.UserData3) == false)
					_record.UserData3 = result.UserData3;
				// test failed - don't run the rest
				if (!result.Passed)
					break;
			}

			_record.TestEnded = DateTime.Now;
			_record.Passed = _record.StepResults.All(r => r.Passed);
			_log.Info("Test completed with result: " + _record.Passed);

			var useDb = ConfigurationManager.AppSettings["UseDatabase"];
			if (useDb.Equals("true"))
			{
				_database.TestedUnits.InsertOnSubmit(_record);
				_database.SubmitChanges();
			}

			context.Dispose();

			_log.Debug("Test run complete");
			if (OnRunCompleted != null)
			{
				OnRunCompleted(this, new TestRunCompletedEventArgs() { SerialNumber = serialNumber, UnitData = unitData });
			}
		}

        public void AddComment(string comment)
		{
			var useDb = ConfigurationManager.AppSettings["UseDatabase"];
			if (useDb.Equals("true"))
			{
				//_database.TestedUnits.InsertOnSubmit(record);
				_record.Comments = comment;
				_database.SubmitChanges();
			}
		}

		public TestExecution()
		{
			Steps = new List<ITestStep>();
			_database = new TestDatabase(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

			// TODO: move this to application startup
			Log.LogInit();
			_log = LogManager.GetLogger(this.GetType().Name);
		}

		private TestDatabase _database;
		private Logger _log;
		private TestEntity _record;
	}

	public class TestRunFactory
	{
		/// <summary>
		/// This instantiates test steps from the app.config
		/// </summary>
		/// <returns></returns>
		public static ITestExecution BuildTestRun(bool mock = false)
		{
			ITestExecution exe = null;
			if (mock)
			{
				exe = new MockTestExecution();
			}
			else
			{
				exe = new TestExecution();
			}

			var testDefinitions = ConfigHandler.ReadTestConfig();
			foreach (var test in testDefinitions.Tests)
			{
				var t = Type.GetType(test.Type);
				var testImpl = Activator.CreateInstance(t) as ITestStep;
				//testImpl.Order = test.Order;
				exe.Steps.Add(testImpl);
			}

			exe.Steps.OrderBy(s => s.Order);
			return exe;
		}

		public static ITestRunContext BuildRunContext()
		{
			// TODO: temp
			var t = Type.GetType("Wellntel.ProdTest.SensorTestRunContext, Wellntel.ProdTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			var impl = Activator.CreateInstance(t) as ITestRunContext;
			impl.Initialize();
			return impl;
		}
	}

	/// <summary>
	///  this is terrible and dumb
	/// </summary>
	public class MockTestExecution : ITestExecution
	{
		public IList<ITestStep> Steps { get; set; }

		public event OnTestStepCompletedHandler OnTestStepCompleted;
		public event OnTestStepStartedHandler OnTestStepStarted;
		public event OnConnectedHandler OnConnected;
		public event OnTestRunCompletedHandler OnRunCompleted;

		public void Execute()
		{
			OnConnected(this, new ConnectedEventArgs() { Id = "00:11:22:33:44:55" });

			System.Threading.Thread.Sleep(5000);
			foreach (var step in Steps)
			{

				var start = new TestStepStartedEventArgs()
				{
					Id = step.Order
				};
				if (OnTestStepStarted != null)
				{
					OnTestStepStarted(this, start);
				}
				var length = (new Random()).Next(1000);
				System.Threading.Thread.Sleep(length);

				var passed = (new Random().Next(100)) < 90;
				var result = new TestStepResult()
				{
					ParentTestId = Guid.NewGuid(),
					Passed = passed,
					Result = "test result",
					TestId = step.Order,
					TestStepResultId = Guid.NewGuid()
				};
				if (OnTestStepCompleted != null)
				{
					OnTestStepCompleted(this, new TestStepCompletedEventArgs() { StepResult = result });
				}
			}

			OnRunCompleted(this, new TestRunCompletedEventArgs());
		}

		public MockTestExecution()
		{
			Steps = new List<ITestStep>();
		}


		public void AddComment(string comment)
		{
			throw new NotImplementedException();
		}
	}
}
