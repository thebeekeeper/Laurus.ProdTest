using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laurus.ProdTest.Core;
using System.Configuration;

namespace Laurus.ProdTest.UnitTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void CreateDatabase()
        {
            var connStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            var db = new TestDatabase(connStr);
            db.Create();
        }

        [TestMethod]
        public void InsertRow()
        {
            var connStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            var db = new TestDatabase(connStr);

            var entity = new TestEntity()
            {
				SerialIdentifier = "test address",
                TestStarted = DateTime.Now,
                TestEnded = DateTime.Now,
                Passed = true
            };
            db.TestedUnits.InsertOnSubmit(entity);
            db.SubmitChanges();
        }

        [TestMethod]
        public void CreateMultipleResults()
        {
            var connStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            var db = new TestDatabase(connStr);

            var entity = new TestEntity()
            {
				SerialIdentifier = "test address",
                TestStarted = DateTime.Now,
                TestEnded = DateTime.Now,
                Passed = true
            };
            db.TestedUnits.InsertOnSubmit(entity);
            db.SubmitChanges();
            var testId = entity.Id;
            entity.StepResults.Add(new TestStepResult()
            {
                Passed = true,
                Result = "blah blah blah",
                TestId = 0,
                ParentTestId = testId
            });
            entity.StepResults.Add(new TestStepResult()
            {
                Passed = false,
                Result = "second result",
                TestId = 1,
                ParentTestId = testId
            });
            db.TestStepResults.InsertOnSubmit(entity.StepResults[0]);
            db.TestStepResults.InsertOnSubmit(entity.StepResults[1]);
            db.SubmitChanges();

            var results = db.TestStepResults.Where(r => r.ParentTestId == testId);
            Assert.IsTrue(results.First().Result.Equals("blah blah blah"));
        }
    }
}
