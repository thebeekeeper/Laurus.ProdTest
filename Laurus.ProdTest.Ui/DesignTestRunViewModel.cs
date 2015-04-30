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
using System.Windows.Input;
using System.Collections.ObjectModel;
using Laurus.ProdTest.Core;

namespace Laurus.ProdTest.Ui
{
    public class DesignTestRunViewModel
    {
        public ICommand StartCommand { get; set; }

        public ObservableCollection<TestStepModel> TestSteps { get; set; }

        public DesignTestRunViewModel()
        {
            var x = new List<TestStepModel>()
            {
                { 
                    new TestStepModel()
                    {
                        Name = "design step 1",
                        Order = 0,
                        StepStatus = TestStepStatus.NOTRUN,
                        Status = "asdf"
                    }
                }
            };
            TestSteps = new ObservableCollection<TestStepModel>(x);
        }

        void _execution_OnTestStepStarted(object sender, TestStepStartedEventArgs args)
        {
        }

        void _execution_OnTestStepCompleted(object sender, TestStepCompletedEventArgs args)
        {
        }

        public void StartTestRun(object param)
        {
        }

    }
}
