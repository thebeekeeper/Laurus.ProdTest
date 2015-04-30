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
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace Laurus.ProdTest.Ui
{
    public class StatusToColorConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = (TestStepStatus)value;
            switch(status)
            {
                case TestStepStatus.NOTRUN:
                    return new SolidColorBrush(Colors.Gray);
                case TestStepStatus.RUNNING:
                    return new SolidColorBrush(Color.FromArgb(255, 63, 114, 186));
                case TestStepStatus.PASSED:
                    return new SolidColorBrush(Color.FromArgb(255, 48, 191, 82));
                case TestStepStatus.FAILED:
                    return new SolidColorBrush(Color.FromArgb(255, 214, 66, 66));
            }
            return new SolidColorBrush(Colors.Black);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class StatusToStringConverter : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = (TestStepStatus)value;
            switch(status)
            {
                case TestStepStatus.NOTRUN:
                    return "NOT RUN";
                case TestStepStatus.RUNNING:
                    return "RUNNING";
                case TestStepStatus.PASSED:
                    return "PASSED";
                case TestStepStatus.FAILED:
                    return "FAILED";
            }
            return "ERROR";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class DoubleToPercentConverter : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value).ToString("P", CultureInfo.InvariantCulture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
