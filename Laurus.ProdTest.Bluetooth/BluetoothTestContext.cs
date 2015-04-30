/*
The MIT License (MIT)

Copyright (c) 2013 Nick Gamroth

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using InTheHand.Net.Sockets;
using Laurus.ProdTest.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.ProdTest.Bluetooth
{
    public class BluetoothTestContext : ITestRunContext
    {
        public bool Initialize()
        {
            var conn = new Connection();
            var disco = new Discovery();
            //_log.Trace("Discovering...");
            var devices = disco.DiscoverDevices();
            if (!devices.Any())
            {
                //_log.Error("No BT devices discovered");
                throw new NoDiscoveredDevicesException("No Bluetooth devices found");
            }

            var disc = default(BluetoothDeviceInfo);
            var targetName = ConfigurationManager.AppSettings["TargetName"];
            if (!devices.Where(d => d.DeviceName.Equals(targetName)).Any())
            {
                foreach (var device in devices)
                {
                    if (disco.GetDeviceName(device).Equals(targetName))
                        disc = device;
                }
            }
            else
            {
                disc = devices.Where(d => d.DeviceName.Equals(targetName)).First();
            }
            if (disc == default(BluetoothDeviceInfo))
                throw new NoTargetFoundException("No device found");

           // _log.Trace("Connecting");
            try
            {
                conn.Connect(disc);
            }
            catch (Exception e)
            {
                //_log.ErrorException("Can't connect to disc", e);
                throw new ConnectionFailedException("Failed to connect" + e);
            }

            return true;
        }

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
