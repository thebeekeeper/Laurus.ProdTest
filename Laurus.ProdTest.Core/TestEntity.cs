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
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Laurus.ProdTest.Core
{
    [Table(Name = "DeviceTest")]
    public class TestEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public Guid Id;
        [Column]
        public DateTime TestStarted;
        [Column]
        public DateTime TestEnded;
        [Column]
        public string SerialIdentifier;
        [Column]
        public bool Passed;
		[Column]
		public string Comments;

		// This link: http://sqlblog.com/blogs/aaron_bertrand/archive/2009/11/19/what-is-so-bad-about-eav-anyway.aspx
        // has an approach we could use here.  Since I don't expect much extra data, I'm ok with just adding a few
        // columns manually for now.
		[Column]
		public string UserData1;
		[Column]
		public string UserData2;
		[Column]
		public string UserData3;

        [Association(Storage = "_stepResults", OtherKey = "ParentTestId")]
        public EntitySet<TestStepResult> StepResults
        {
            get { return this._stepResults; }
            set { this._stepResults.Assign(value); }
        }

        public TestEntity()
        {
            _stepResults = new EntitySet<TestStepResult>();
            Id = Guid.NewGuid();
        }

        private EntitySet<TestStepResult> _stepResults;
    }

    [Table(Name = "TestStepResult")]
    public class TestStepResult
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public Guid TestStepResultId { get; set; }
        [Column]
        public Guid ParentTestId { get; set; }
        /// <summary>
        /// Integer reference to the type of test this was
        /// </summary>
        [Column]
        public int TestId { get; set; }
        [Column]
        public string Result { get; set; }
        [Column]
        public bool Passed { get; set; }

        // TODO: make this a column? probabl not - this is a hack to pass the serial number back up a level
		public string SerialNumber { get; set; }
        // same hack
		public string UnitData { get; set; }
		public string UserData1 { get; set; }
		public string UserData2 { get; set; }
		public string UserData3 { get; set; }
    }
}
