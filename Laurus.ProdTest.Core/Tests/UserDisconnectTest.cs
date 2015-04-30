using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class UserDisconnectTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "User Disconnect Command"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            _log.Info("Starting UserDisconnectTest");
			//conn.SendCommand(ProtocolSchema.Commands.TY_CMD_USER_DISCONNECT);
            Console.Write("Is the tag alarming? ");
            // the tag should not alarm
            bool result = Console.ReadLine().ToLower().Equals("n");
            result = result & !conn.IsConnected;
            return new TestStepResult() { Passed = result };
        }
        
        public UserDisconnectTest()
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
