using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laurus.ProdTest.Core;

namespace Laurus.ProdTest.UnitTests
{
    [TestClass]
    public class AudioTests
    {
        [TestMethod]
        public void RecordAudio()
        {
            var file = @"e:\temp\output.wav";
            var a = new Audio(file);
            a.Record(1000);
            var f = System.IO.File.Exists(file);
            Assert.IsTrue(f);
        }
    }
}
