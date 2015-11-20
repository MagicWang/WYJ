using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WYJ.Core.Extensions;

namespace WYJ.Windows.Controls
{
    /// <summary>
    /// 给控件添加水印文本
    /// </summary>
    public class WatermarkAdorner : Adorner
    {
        public WatermarkAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            if (AdornedElement is TextBox)
            {
                SetContentProperty(AdornedElement, "Text");
            }
            else if (AdornedElement is PasswordBox)
            {
                SetContentProperty(AdornedElement, "Password");
            }
            AdornedElement.GotKeyboardFocus += (s, e) => InvalidateVisual();
            AdornedElement.LostKeyboardFocus += (s, e) => InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            var propertyName = GetContentProperty(AdornedElement);
            var value = AdornedElement.GetProperty(propertyName);
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                var fmt = new FormattedText(GetText(AdornedElement),
                CultureInfo.CurrentCulture,
                (AdornedElement as Control).FlowDirection,
                (AdornedElement as Control).FontFamily.GetTypefaces().FirstOrDefault(),
                (AdornedElement as Control).FontSize,
                GetForeground(AdornedElement));
                fmt.SetFontStyle(GetFontStyle(AdornedElement));

                dc.DrawRectangle(GetBackground(AdornedElement), null, new Rect(
                    new Point((AdornedElement as Control).Padding.Left + 4, (AdornedElement as Control).Padding.Top + 1),
                    new Size(fmt.Width, fmt.Height)));

                dc.DrawText(fmt, new Point((AdornedElement as Control).Padding.Left + 4, (AdornedElement as Control).Padding.Top + 1));
            }
        }

        public static void OnTextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as FrameworkElement;
            var adorner = new WatermarkAdorner(source);
            adorner.SetBinding(UIElement.VisibilityProperty, new System.Windows.Data.Binding("Visibility") { Source = source });
            source.Loaded += (s, e1) => AdornerLayer.GetAdornerLayer(source).Add(adorner);
        }

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }
        /// <summary>
        /// 水印文本
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(WatermarkAdorner), new UIPropertyMetadata(null, OnTextPropertyChangedCallback));


        public static string GetContentProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentPropertyProperty);
        }

        public static void SetContentProperty(DependencyObject obj, string value)
        {
            obj.SetValue(ContentPropertyProperty, value);
        }
        /// <summary>
        /// 关联控件内容属性,默认为Text(使用反射求出关联控件此属性的值，来判断是否需要绘制水印文本)
        /// </summary>
        public static readonly DependencyProperty ContentPropertyProperty =
            DependencyProperty.RegisterAttached("ContentProperty", typeof(string), typeof(WatermarkAdorner), new PropertyMetadata("Text"));

        public static Brush GetForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundProperty);
        }

        public static void SetForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(WatermarkAdorner), new UIPropertyMetadata(Brushes.Gray));

        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(WatermarkAdorner), new UIPropertyMetadata(Brushes.Transparent));

        public static FontStyle GetFontStyle(DependencyObject obj)
        {
            return (FontStyle)obj.GetValue(FontStyleProperty);
        }

        public static void SetFontStyle(DependencyObject obj, FontStyle value)
        {
            obj.SetValue(FontStyleProperty, value);
        }

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.RegisterAttached("FontStyle", typeof(FontStyle), typeof(WatermarkAdorner), new UIPropertyMetadata(FontStyles.Italic));
    }
}
