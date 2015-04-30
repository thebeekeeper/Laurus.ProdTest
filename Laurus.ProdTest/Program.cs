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
using Laurus.ProdTest.Core;


namespace Laurus.ProdTest
{
	class Program
	{
		static void Log(string msg)
		{
			Console.WriteLine(msg);
		}

		static void Log(IEnumerable<byte> bytes)
		{
			bytes.ToList().ForEach(b => Console.Write(b.ToString() + "\t"));
			Console.WriteLine();
		}

		static void Main(string[] args)
		{
#if _DEBUG
            if (ConfigSecurity.CheckDigitalSignature() == false)
            {
                Log("Config file signature check failed. Exiting");
                return;
            }
#endif
			try
			{
				var testRun = TestRunFactory.BuildTestRun();
				testRun.Execute();
			}
			catch (NoDiscoveredDevicesException)
			{
				Log("No devices found");
			}
			catch (ConnectionFailedException)
			{
				Log("Connection to device failed");
			}
			catch (NoTargetFoundException)
			{
				Log("No target devices found");
			}

			Console.WriteLine("Press a key to exit");
			Console.ReadKey(false);
		}
	}
}
