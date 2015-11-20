using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace WYJ.Windows.Controls
{
    /// <summary>
    /// 封装AxWindowsMediaPlayer
    /// </summary>
    public class WindowsMediaPlayer : ContentControl
    {
        private AxWMPLib.AxWindowsMediaPlayer AxWMP;
        public WMPLib.WindowsMediaPlayer WMP { get { return AxWMP.GetOcx() as WMPLib.WindowsMediaPlayer; } }
        public WindowsMediaPlayer()
        {
            WindowsFormsHost host = new WindowsFormsHost();
            AxWMP = new AxWMPLib.AxWindowsMediaPlayer();
            host.Child = AxWMP;
            Content = host;
            Loaded += WindowsMediaPlayer_Loaded;
            Unloaded += WindowsMediaPlayer_Unloaded;
        }

        private void WindowsMediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            WMP.URL = Url;

            WMP.settings.setMode("loop", IsLoop);
            WMP.PlayStateChange += WMP_PlayStateChange;
        }

        private void WMP_PlayStateChange(int NewState)
        {
            switch (NewState)
            {
                case 3:
                    WMP.fullScreen = IsFullScreen;
                    break;
                default:
                    break;
            }
        }

        private void WindowsMediaPlayer_Unloaded(object sender, RoutedEventArgs e)
        {
            WMP.controls.stop();
        }

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
        /// <summary>
        /// 指定媒体位置，本机或网络地址
        /// </summary>
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(WindowsMediaPlayer), new PropertyMetadata(""));

        public bool IsLoop
        {
            get { return (bool)GetValue(IsLoopProperty); }
            set { SetValue(IsLoopProperty, value); }
        }
        /// <summary>
        /// 是否循环播放
        /// </summary>
        public static readonly DependencyProperty IsLoopProperty =
            DependencyProperty.Register("IsLoop", typeof(bool), typeof(WindowsMediaPlayer), new PropertyMetadata(false));

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }
        /// <summary>
        /// 是否全屏
        /// </summary>
        public static readonly DependencyProperty IsFullScreenProperty =
            DependencyProperty.Register("IsFullScreen", typeof(bool), typeof(WindowsMediaPlayer), new PropertyMetadata(false));
    }
}
