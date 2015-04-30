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


namespace Laurus.ProdTest.Ui
{
    //public class TestStepViewModel : ReactiveObject
    //{
    //    public ICommand RunTest { get; protected set; }

    //    private TestStepStatus _executionState;
    //    public TestStepStatus ExecutionState
    //    {
    //        get { return _executionState; }
    //        set { this.RaiseAndSetIfChanged(x => x._executionState, value); }
    //    }

    //    private bool _readyToRun;
    //    public bool ReadyToRun
    //    {
    //        get { return _readyToRun; }
    //        set { this.RaiseAndSetIfChanged(x => x._readyToRun, value); }
    //    }

    //    ObservableAsPropertyHelper<System.Windows.Visibility> _runVisibility;
    //    public Visibility RunVisibility
    //    {
    //        get { return _runVisibility.Value; }
    //    }

    //    public TestStepViewModel()
    //    {

    //    }
    //}

    public enum TestStepStatus
    {
        PASSED,
        FAILED,
        RUNNING,
        NOTRUN
    }
}
