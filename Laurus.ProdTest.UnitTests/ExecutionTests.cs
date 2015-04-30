using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laurus.ProdTest.Core;


namespace Laurus.ProdTest.UnitTests
{
    [TestClass]
    public class ExecutionTests
    {
        [TestMethod]
        public void BuildTestRun()
        {
            var testRun = TestRunFactory.BuildTestRun();
            Assert.IsNotNull(testRun.Steps.First());
        }

        [TestMethod]
        public void VerifyMultipleStepOrder()
        {
            var testRun = TestRunFactory.BuildTestRun();
            Assert.IsTrue(testRun.Steps.Count == 2);
            Assert.IsTrue(testRun.Steps[0].Order == 0);
            Assert.IsTrue(testRun.Steps[1].Order == 1);
            //Assert.IsTrue(testRun.Steps[0].GetType().Equals(typeof(BuzzerTest)));
            //Assert.IsTrue(testRun.Steps[1].GetType().Equals(typeof(CommandTest)));
        }

        [TestMethod]
        public void RunExecution()
        {
            var testRun = TestRunFactory.BuildTestRun();
            testRun.Execute();
        }
    }
}
