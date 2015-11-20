using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WYJ.Windows.Controls;

namespace System.Diagnostics
{
    public class LogControlTraceListener : TraceListener
    {
        public static List<LogControl> LogControls = new List<LogControl>();
        public override void Write(string message)
        {
            LogControls.ForEach(l => l.paragraph.Inlines.Add(message));
        }

        public override void WriteLine(string message)
        {
            LogControls.ForEach(l =>
            {
                l.paragraph.Inlines.Add(message);
                l.paragraph.Inlines.Add(new Windows.Documents.LineBreak());
            });
        }
    }
}
