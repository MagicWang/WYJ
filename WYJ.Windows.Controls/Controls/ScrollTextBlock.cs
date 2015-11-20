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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WYJ.Windows.Controls
{
    [TemplatePart(Name = "textblock1", Type = typeof(TextBlock))]
    [TemplatePart(Name = "textblock2", Type = typeof(TextBlock))]
    /// <summary>
    /// 走马灯文本控件
    /// </summary>
    public class ScrollTextBlock : Control
    {
        private TextBlock tb1;
        private TextBlock tb2;
        static ScrollTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollTextBlock), new FrameworkPropertyMetadata(typeof(ScrollTextBlock)));
        }
        public ScrollTextBlock()
        {
            SizeChanged += ScrollTextBlock_SizeChanged;
        }

        #region DependencyProperty
        /// <summary>
        /// 走马灯文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ScrollTextBlock), new PropertyMetadata(""));
        /// <summary>
        /// 每秒滚动过多少个像素点
        /// </summary>
        public int Speed
        {
            get { return (int)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int), typeof(ScrollTextBlock), new PropertyMetadata(100));
        #endregion

        void ScrollTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tb1 = tb1 ?? VisualTreeHelperExtension.GetChildObject<TextBlock>(this, "textBlock1");
            tb2 = tb2 ?? VisualTreeHelperExtension.GetChildObject<TextBlock>(this, "textBlock2");
            if (tb1 == null || tb2 == null)
                return;
            tb2.Visibility = Visibility.Hidden;

            var time1 = tb1.ActualWidth / Speed;
            var time2 = (ActualWidth + tb1.ActualWidth) / Speed;

            var storyboard1 = CreateAnimation(tb1, time1, time2);
            storyboard1.Begin();
            var storyboard2 = CreateAnimation(tb2, time1, time2);
            storyboard2.BeginTime = TimeSpan.FromSeconds(time1);
            storyboard2.Begin();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(time1 + 0.1);
            timer.Tick += (s, ee) =>
            {
                timer.Stop();
                tb2.Visibility = Visibility.Visible;
            };
            timer.Start();
        }
        private Storyboard CreateAnimation(TextBlock tb, double time1, double time2)
        {
            var sb = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(ActualWidth, TimeSpan.FromSeconds(0)));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(ActualWidth - tb.ActualWidth, TimeSpan.FromSeconds(time1)));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(-tb.ActualWidth, TimeSpan.FromSeconds(time2)));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(-tb.ActualWidth, TimeSpan.FromSeconds(time1 * 2)));
            Storyboard.SetTarget(animation, tb);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)", TextBlock.RenderTransformProperty, TranslateTransform.XProperty));
            sb.Children.Add(animation);
            sb.RepeatBehavior = RepeatBehavior.Forever;
            return sb;
        }
    }
}
