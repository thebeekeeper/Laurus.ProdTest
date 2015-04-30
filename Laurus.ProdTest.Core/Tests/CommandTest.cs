using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class CommandTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Command"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            return default(TestStepResult);
        }


        TestStepResult ITestStep.Execute(ref ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
