using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WYJ.Windows.Controls;
using CefSharp;

namespace WYJ.Samples
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            browser.Initialized += browser_Initialized;
            browser.Loaded += browser_Loaded;
            browser.IsBrowserInitializedChanged += browser_IsBrowserInitializedChanged;
            browser.LoadingStateChanged += browser_LoadingStateChanged;
            browser.FrameLoadStart += browser_FrameLoadStart;
            browser.FrameLoadEnd += browser_FrameLoadEnd;
            browser.MenuHandler = new MenuHandle();
            browser.RequestHandler = new RequestHandler();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                browser.ShowDevTools();
            }
        }

        void browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            Console.WriteLine("load changed:" + e.IsLoading);
        }

        void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Console.WriteLine("FrameLoadEnd");
        }

        void browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            Console.WriteLine("FrameLoadStart");
        }

        void browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("IsBrowserInitializedChanged");
            browser.Load("https://bitbucket.org/xilium/xilium.cefglue/issues/attachments/122/xilium/xilium.cefglue/1443103104.73/122/test.html");
            //browser.Load("http://192.168.1.170:30901");
        }

        void browser_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("loaded");
        }

        void browser_Initialized(object sender, EventArgs e)
        {
            Console.WriteLine("Init");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            browser.Load("https://bitbucket.org/xilium/xilium.cefglue/issues/attachments/122/xilium/xilium.cefglue/1443103104.73/122/test.html");
            //browser.ExecuteScriptAsync(String.Format("{0}('{1}');", "mapToolBarProxy", "initCluesItems"));
            //browser.RegisterJsObject("test", new object());
        }
    }
    public class MenuHandle : IContextMenuHandler
    {
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {

        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return true;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }
    }
}
