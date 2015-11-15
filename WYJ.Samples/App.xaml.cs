using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WYJ.Samples
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //var cefPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Sdk\Cef");
            //CefSharp.CefSettings settings = new CefSharp.CefSettings();
            //settings.LocalesDirPath = System.IO.Path.Combine(cefPath, "locales");
            //settings.Locale = "zh-CN";
            //settings.LogFile = System.IO.Path.Combine(cefPath, "Log.txt");
            //settings.LogSeverity = CefSharp.LogSeverity.Verbose;
            //settings.CachePath = System.IO.Path.Combine(cefPath, "cache");
            //settings.BrowserSubprocessPath = System.IO.Path.Combine(cefPath, "CefSharp.BrowserSubprocess.exe");
            ////settings.CefCommandLineArgs.Add("enable-deferred-image-decoding", "1");
            //CefSharp.Cef.Initialize(settings);
        }
    }
}
