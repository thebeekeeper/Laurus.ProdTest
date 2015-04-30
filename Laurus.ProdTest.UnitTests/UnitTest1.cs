using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laurus.ProdTest.Core;

namespace Laurus.ProdTest.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RunDiscovery()
        {
            //var disco = new Core.Discovery();
            //var devices = disco.DiscoverDevices();
            //Assert.IsFalse(devices.First().DeviceName.Equals("tyDisc"));
        }

        [TestMethod]
        public void Discover_Connect_Read()
        {
            //var disco = new Core.Discovery();
            //var devices = disco.DiscoverDevices();
            //var connection = new Core.Connection();
            //var stream = connection.Connect(devices.First());
            //var bytes = connection.Read(stream);
            //Assert.IsTrue(bytes.Any());
        }
        
        [TestMethod]
        public void VerifyRegexValid()
        {
            //string addr = "F4:FC:32:1B:1F:16";
            //Assert.IsTrue(addr.IsBluetoothAddress());
        }

        [TestMethod]
        public void VerifyRegexNotAddr()
        {
            //string addr = "tyDisc";
            //Assert.IsFalse(addr.IsBluetoothAddress());
        }
    }
}
