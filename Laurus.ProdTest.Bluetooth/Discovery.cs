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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net.Sockets;
using System.Text.RegularExpressions;

namespace Laurus.ProdTest.Core
{
    public class Discovery
    {
        public IEnumerable<BluetoothDeviceInfo> DiscoverDevices()
        {
            var client = new BluetoothClient();
            var peers = client.DiscoverDevices(5);
            client.Close();
            return peers;
        }

        /// <summary>
        /// if the name didn't come in yet we get an address
        /// formatted like "F4:FC:32:1B:1F:16".  if the given
        /// device has a name of that format, ask for it again
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public string GetDeviceName(BluetoothDeviceInfo device)
        {
            if (device.DeviceName.IsBluetoothAddress())
            {
                System.Threading.Thread.Sleep(1000);
                device.Refresh();
            }
            return device.DeviceName;
        }

    }

    public static class StringExtensions
    {
        public static bool IsBluetoothAddress(this string str)
        {
            Regex re = new Regex(@"..:..:..:..:..:..");
            MatchCollection mc = re.Matches(str);
            return mc.Count > 0;
        }
    }
}
