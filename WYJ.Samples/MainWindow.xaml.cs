using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WYJ.Core.Themes;
using WYJ.Windows;
using WYJ.Windows.Controls;
using WYJ.Windows.Themes;

namespace WYJ.Samples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            comboBox.ItemsSource = new string[] { "Generic", "Black", "DarkBlue" };
            var pcv=new PagedCollectionView(Enumerable.Range(1, 100).ToList());
            dataPager.Source = pcv;
            listBox.ItemsSource = pcv;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeManager.SetThemeName(this, comboBox.SelectedItem.ToString());
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }
    }
}
