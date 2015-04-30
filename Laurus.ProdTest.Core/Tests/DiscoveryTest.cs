using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laurus.ProdTest.Core.Tests
{
    public class DiscoveryTest : ITestStep
    {
        int ITestStep.Order
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string ITestStep.Name
        {
            get { throw new NotImplementedException(); }
        }

        TestStepResult ITestStep.Execute(Connection conn)
        {
            throw new NotImplementedException();
        }

        TestStepResult ITestStep.Execute(ref ConnectionInfo connectionInfo)
        {
            var result = new TestStepResult();
            var disco = new Discovery();
            var devices = disco.DiscoverDevices();
            if (!devices.Any())
            {
                _log.Error("No BT devices discovered");
                result.Passed = false;
                //throw new NoDiscoveredDevicesException("No Bluetooth devices found");
            }
            connectionInfo.DiscoveredDevices = devices;
            result.Passed = true;
            return result;
        }

        public DiscoveryTest()
        {
              _log = NLog.LogManager.GetLogger(this.GetType().Name);
        }

        private NLog.Logger _log;
    }
}
