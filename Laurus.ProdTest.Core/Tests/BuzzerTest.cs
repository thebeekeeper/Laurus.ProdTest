using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class BuzzerTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Buzzer"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
			//conn.SendCommand(Laurus.ProdTest.Core.ProtocolSchema.Commands.TY_CMD_BEEP);
			return default(TestStepResult);
        }



        TestStepResult ITestStep.Execute(ref ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
