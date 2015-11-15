using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WYJ.Windows;

namespace WYJ.Samples.Controls
{
    /// <summary>
    /// LogSample.xaml 的交互逻辑
    /// </summary>
    public partial class LogSample : UserControl
    {
        public LogSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.Trace(LogHelper.DefaultSwitch, System.Diagnostics.TraceLevel.Error, "error");
            LogHelper.Trace(LogHelper.DefaultSwitch, System.Diagnostics.TraceLevel.Warning, "warning");
            LogHelper.Trace(LogHelper.DefaultSwitch, System.Diagnostics.TraceLevel.Info, "info");
            LogHelper.Trace(LogHelper.DefaultSwitch, System.Diagnostics.TraceLevel.Verbose, "verbose");
            LogHelper.Flush();
        }
    }
}
