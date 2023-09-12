using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Log
{

    public class MyLogger : ILogEventSink
    {
        public Serilog.Core.Logger? Logger { get; private set; }
        public ConcurrentQueue<string> Events { get; } = new ConcurrentQueue<string>();
        private static object lockObj = new Object();
        private ITextFormatter _formatter = new MessageTemplateTextFormatter("{Timestamp} [{Level}] {Message}{Exception}\"");
        private static MyLogger? _MyLogger;
        public static MyLogger GetInstance()
        {
            lock (lockObj)
            {
                _MyLogger ??= new MyLogger();
                
            }
            return _MyLogger;
        }

        private MyLogger()
        {
            Logger ??= new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .WriteTo.Sink(this)
                .CreateLogger();
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            var render = new StringWriter();
            _formatter.Format(logEvent, render);
            Events.Enqueue(render.ToString());

        }
    }
}
