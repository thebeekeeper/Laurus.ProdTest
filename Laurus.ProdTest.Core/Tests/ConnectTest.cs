using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    /// <summary>
    /// Dummy test case for the UI -- get rid of this...
    /// </summary>
    public class ConnectTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Connect"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            return new TestStepResult() { Passed = true };
        }

        TestStepResult ITestStep.Execute(ref ConnectionInfo connectionInfo)
        {
            return new TestStepResult() { Passed = true };
        }
    }
}
