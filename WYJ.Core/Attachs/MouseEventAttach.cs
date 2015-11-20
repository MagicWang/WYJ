using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WYJ.Core.Attachs
{
    public class MouseEventAttach
    {
        public static ICommand GetClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ClickCommandProperty);
        }
        public static void SetClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClickCommandProperty, value);
        }
        /// <summary>
        /// 给UIElement附加单击时执行的命令
        /// </summary>
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.RegisterAttached("ClickCommand", typeof(ICommand), typeof(MouseEventAttach), new PropertyMetadata(null, OnClickCommandChangedCallback));
        private static void OnClickCommandChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as UIElement;
            if (c != null)
            {
                c.PreviewMouseLeftButtonDown -= c_PreviewMouseLeftButtonDown;
                c.PreviewMouseLeftButtonDown += c_PreviewMouseLeftButtonDown;
            }
        }
        public static object GetClickCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(ClickCommandParameterProperty);
        }

        public static void SetClickCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ClickCommandParameterProperty, value);
        }
        public static readonly DependencyProperty ClickCommandParameterProperty =
            DependencyProperty.RegisterAttached("ClickCommandParameter", typeof(object), typeof(MouseEventAttach), new PropertyMetadata(null));

        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        /// <summary>
        /// 给UIElement附加双击时执行的命令
        /// </summary>
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(MouseEventAttach), new PropertyMetadata(null, OnDoubleClickCommandChangedCallback));
        public static object GetDoubleClickCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DoubleClickCommandParameterProperty);
        }
        public static void SetDoubleClickCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DoubleClickCommandParameterProperty, value);
        }
        public static readonly DependencyProperty DoubleClickCommandParameterProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommandParameter", typeof(object), typeof(MouseEventAttach), new PropertyMetadata(null));
        private static void OnDoubleClickCommandChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as UIElement;
            if (c != null)
            {
                c.PreviewMouseLeftButtonDown -= c_PreviewMouseLeftButtonDown;
                c.PreviewMouseLeftButtonDown += c_PreviewMouseLeftButtonDown;
            }
        }
        static void c_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickCount++;
            Delay(400, l =>
                {
                    if (clickCount == 1)
                    {
                        var cmd = ((sender as UIElement).GetValue(ClickCommandProperty) as ICommand);
                        var param = (sender as UIElement).GetValue(ClickCommandParameterProperty);
                        if (cmd != null)
                            cmd.Execute(param);
                    }
                    else if (clickCount == 2)
                    {
                        var cmd = ((sender as UIElement).GetValue(DoubleClickCommandProperty) as ICommand);
                        var param = (sender as UIElement).GetValue(DoubleClickCommandParameterProperty);
                        if (cmd != null)
                            cmd.Execute(param);
                    }
                    clickCount = 0;
                });
        }
        static int clickCount;
        static void Delay(int ms, Action<object> action, object param = null)
        {
            Task.Factory.StartNew(() => Thread.Sleep(ms)).ContinueWith(l => Application.Current.Dispatcher.Invoke(action, param));
        }
    }
}
