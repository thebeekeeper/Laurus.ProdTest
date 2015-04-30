using Laurus.ProdTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.ProdTest.Sample
{
    public class SampleStep : ITestStep
    {
		public int Order { get; set; }

		public string Name { get { return "Sample Step"; } }

		public TestStepResult Execute(ITestRunContext context)
		{
			return new TestStepResult()
			{
				Passed = true,
			};
		}
	}
}
