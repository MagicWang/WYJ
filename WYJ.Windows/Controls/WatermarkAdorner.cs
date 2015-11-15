using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WYJ.Windows.Controls
{
    /// <summary>
    /// 给TextBox或Password添加水印文本
    /// </summary>
    public class WatermarkAdorner : Adorner
    {
        public WatermarkAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.IsHitTestVisible = false;
            if (AdornedElement is TextBox || AdornedElement is PasswordBox)
            {
                AdornedElement.GotFocus += (s, e) => InvalidateVisual();
                AdornedElement.LostFocus += (s, e) => InvalidateVisual();
            }
            else
                throw new InvalidOperationException("要关联的控件不支持,只支持TextBox和PasswordBox");
        }

        protected override void OnRender(DrawingContext dc)
        {
            string text = null;
            if (AdornedElement is TextBox)
                text = (AdornedElement as TextBox).Text;
            else if (AdornedElement is PasswordBox)
                text = (AdornedElement as PasswordBox).Password;
            if (string.IsNullOrEmpty(text) && !AdornedElement.IsFocused)
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
            var source = d as Control;
            source.Loaded += (s, e1) => AdornerLayer.GetAdornerLayer(source).Add(new WatermarkAdorner(source));
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
