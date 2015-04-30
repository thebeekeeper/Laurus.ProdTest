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
    public class ConfigTests
    {
        [TestMethod]
        public void TempTest()
        {
			Assert.Fail("fix this test");
			//var serviceConfigSection = ConfigurationManager.GetSection("TestsSection") as TestRunConfigurationSection;

			//TestConfig serviceConfig = serviceConfigSection.Tests[0];
			//Assert.AreEqual(0, serviceConfig.Order);
        }

        [TestMethod]
        public void Test_Read_From_File()
        {
			Assert.Fail("fix this test");
			//var tests = ConfigHandler.ReadTestFromFile(@"e:\sample.config");
			//Assert.IsTrue(tests.Count == 3);
        }

		[TestMethod]
		public void CheckConfigSigning()
		{
			//var isSigned = ConfigSecurity.CheckDigitalSignature(@"E:\workspace\Wellntel\signing\app.config");
			//Assert.IsTrue(isSigned);
		}
    }
}
