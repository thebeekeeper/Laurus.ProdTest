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
using NLog.Config;
using NLog.Targets;
using NLog;
using System.Configuration;

namespace Laurus.ProdTest.Core
{
    public class Log
    {
        // TODO: it seems like it cant do console and file logging at the same time?
        public static void LogInit()
        {
            // TODO: move this config somewhere better and pass in the params
            if (ConfigurationManager.AppSettings["LogDatabase"].Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                DatabaseTarget target = new DatabaseTarget();
                DatabaseParameterInfo param;

                target.ConnectionString = ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
                target.CommandText = "insert into Log(timestamp,loglevel,logger,message) values(@time_stamp, @level, @logger, @message);";

                param = new DatabaseParameterInfo();
                param.Name = "@time_stamp";
                param.Layout = "${date}";
                target.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@level";
                param.Layout = "${level}";
                target.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@logger";
                param.Layout = "${logger}";
                target.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@message";
                param.Layout = "${message}";
                target.Parameters.Add(param);

                NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target);
            }

            if (ConfigurationManager.AppSettings["LogConsole"].Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                var console = new ColoredConsoleTarget();
                console.Layout = "${logger} \t ${message}";
                NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(console, LogLevel.Trace);
            }

            if (ConfigurationManager.AppSettings["LogFile"].Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                var file = new FileTarget();
                file.FileName = "testlog.log";
                file.Layout = "${logger} \t ${message}";
                NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(file, LogLevel.Trace);
            }
        }
    }
}
