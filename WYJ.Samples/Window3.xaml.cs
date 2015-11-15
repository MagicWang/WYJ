using CefSharp;
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
using System.Windows.Shapes;

namespace WYJ.Samples
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public static Window3 Win;
        public Window3()
        {
            InitializeComponent();
            browser.RegisterAsyncJsObject("external", new BoundObject(), false);
            Loaded += Window3_Loaded;
        }

        void Window3_Loaded(object sender, RoutedEventArgs e)
        {
            Win = this;
            browser.JsDialogHandler = new JsDialogHandler();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            browser.ExecuteScriptAsync("rectSelect()");
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F12)
                browser.ShowDevTools();
        }
        public object GetZoomLevel()
        {
            var task = browser.EvaluateScriptAsync("getZoomLevel()");
            task.Wait();
            var level = !task.IsFaulted && task.Result.Success ? task.Result.Result : null;
            return level;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var level = GetZoomLevel();
            Console.WriteLine(level);
        }
    }
    public class BoundObject
    {
        public void OnSelected(object param)
        {
            var level = Window3.Win.GetZoomLevel();
            Console.WriteLine(level);
        }
    }
    public class JsDialogHandler : IJsDialogHandler
    {
        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, string acceptLang, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            switch (dialogType)
            {
                case CefJsDialogType.Alert:
                    MessageBox.Show(messageText);
                    suppressMessage = true;
                    return false;
                case CefJsDialogType.Confirm:
                    var dr = MessageBox.Show(messageText, "提示", MessageBoxButton.YesNo);
                    if (dr == MessageBoxResult.Yes)
                        callback.Continue(true);
                    else
                        callback.Continue(false);
                    suppressMessage = false;
                    return true;
                case CefJsDialogType.Prompt:
                    MessageBox.Show("系统不支持prompt形式的提示框", "UTMP系统提示");
                    break;
            }
            //如果suppressMessage被设置为true，并且函数返回值为false，将阻止页面打开JS的弹出窗口。
            //如果suppressMessage被设置为false，并且函数返回值也是false，页面将会打开这个JS弹出窗口。
            suppressMessage = true;
            return false;
        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            return false;
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {

        }
    }
}
