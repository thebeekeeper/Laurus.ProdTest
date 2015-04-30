using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class FactoryResetTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Reset"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            _log.Info("Starting FactoryResetTest");
			//conn.SendCommand(ProtocolSchema.Commands.TY_CMD_RESET_TO_FACTORY);
            return new TestStepResult() { Passed = true };
        }

        public FactoryResetTest()
        {
            _log = NLog.LogManager.GetLogger(this.GetType().Name);
        }

        private NLog.Logger _log;


        TestStepResult ITestStep.Execute(ref ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
