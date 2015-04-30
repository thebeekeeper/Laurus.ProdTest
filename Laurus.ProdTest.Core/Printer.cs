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
using System.Drawing;
using System.Drawing.Printing;

namespace Laurus.ProdTest.Core
{
    public class Printer
    {
        // printer is 300dpi, 4in wide

        public Printer()
        {
            _printDoc = new PrintDocument();
            _printDoc.PrintPage += new PrintPageEventHandler(printer_PrintPage);
        }

        public void PrintLabel(string bdAddr)
        {
            _printDoc.Print();
        }

        private Bitmap CreateLabel(string bdaddr)
        {
            int width = 300 * 4;
            // 1 inch high?
            int height = 300 * 1;
            var bmp = new Bitmap(width, height);
            var g = Graphics.FromImage(bmp);
            g.DrawString(bdaddr, null, new SolidBrush(Color.Black), new PointF(10.0f, 50.0f));

            return bmp;
        }

        void printer_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(CreateLabel("blah"), new Point(0, 0));
        }

        private PrintDocument _printDoc;
    }
}
