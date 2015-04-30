using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class BatteryLifeTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Battery Life"; } }
       
        TestStepResult ITestStep.Execute(Connection conn)
        {
            _log.Info("Starting BatteryLifeTest");

            var rval = new TestStepResult();
			//var resp = conn.SendCommand(ProtocolSchema.Commands.TY_CMD_DISABLE_ALARM).ToArray();

			//if (resp[3] == ProtocolSchema.TY_ACK)
			//	rval.Passed = true;
			//else
			//	rval.Passed = false;

            return rval;
        }

        public BatteryLifeTest()
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
