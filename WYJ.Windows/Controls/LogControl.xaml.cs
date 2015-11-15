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

namespace WYJ.Windows.Controls
{
    /// <summary>
    /// LogControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogControl : UserControl
    {
        public LogControl()
        {
            InitializeComponent();
            System.Diagnostics.LogControlTraceListener.LogControls.Add(this);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            paragraph.Inlines.Clear();
        }
    }
}
