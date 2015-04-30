using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class DisconnectReconnectTest : ITestStep
    {
        int ITestStep.Order { get; set; }

        string ITestStep.Name { get { return "Force disconnect, reconnect"; } }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            _log.Info("Starting DisconnectReconnectTest");
            var device = conn.Device;
            conn.Disconnect();
            if (conn.IsConnected)
                System.Threading.Thread.Sleep(5000);
            _log.Info("Disconnected, waiting");
            System.Threading.Thread.Sleep(1000);
            _log.Info("Reconnecting");
            conn.Connect(device);

            var result = conn.IsConnected;
            _log.Info("Reconnection result: " + result);
            return new TestStepResult() { Passed = result };
        }
        
        public DisconnectReconnectTest()
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
