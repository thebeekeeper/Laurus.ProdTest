using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class AlarmTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Alarm Test"; } }
       
        TestStepResult ITestStep.Execute(Connection conn)
        {
            _log.Info("Starting AlarmTest");
            //conn.SendCommand(ProtocolSchema.Commands.TY_CMD_SET_ALARM_PROXIMITY_ENABLED);
			//conn.SendCommand(ProtocolSchema.Commands.TY_CMD_ENABLE_ALARM);
			//var ack = conn.SendCommand(ProtocolSchema.Commands.TY_CMD_DO_ALARM);
            _log.Info("Sleeping for 30 seconds...");
            System.Threading.Thread.Sleep(30000);
            //conn.SendCommand(ProtocolSchema.Commands.TY_CMD_DISABLE_ALARM);
            var rval = new TestStepResult();
            rval.Passed = true;
            return rval;
        }

        public AlarmTest()
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
