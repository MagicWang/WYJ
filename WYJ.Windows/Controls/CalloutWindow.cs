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
    [TemplatePart(Name = HeaderContainerName, Type = typeof(Grid))]
    [TemplatePart(Name = CloseButtonName, Type = typeof(Button))]
    public class CalloutWindow : Window
    {
        #region 模板部件常量
        private const string HeaderContainerName = "PART_HeaderContainer";
        private const string CloseButtonName = "PART_CloseButton";
        #endregion

        #region 私有变量
        private Button closeBtn;
        #endregion

        #region 依赖属性
        /// <summary>
        /// 标题栏背景色
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(CalloutWindow), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2B2F3E"))));
        /// <summary>
        /// 气泡箭头点X轴屏幕坐标
        /// </summary>
        public double PointX
        {
            get { return (double)GetValue(PointXProperty); }
            set { SetValue(PointXProperty, value); }
        }
        public static readonly DependencyProperty PointXProperty =
            DependencyProperty.Register("PointX", typeof(double), typeof(CalloutWindow), new PropertyMetadata(0d));
        /// <summary>
        /// 气泡箭头点Y轴屏幕坐标
        /// </summary>
        public double PointY
        {
            get { return (double)GetValue(PointYProperty); }
            set { SetValue(PointYProperty, value); }
        }
        public static readonly DependencyProperty PointYProperty =
            DependencyProperty.Register("PointY", typeof(double), typeof(CalloutWindow), new PropertyMetadata(0d));
        #endregion

        #region 构造函数
        static CalloutWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalloutWindow), new FrameworkPropertyMetadata(typeof(CalloutWindow)));
        }
        public CalloutWindow()
        {
            WindowStyle = System.Windows.WindowStyle.None;
            AllowsTransparency = true;
            ShowInTaskbar = false;
            Loaded += CalloutWindow_Loaded;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            closeBtn = Template.FindName(CloseButtonName, this) as Button;
            if (closeBtn != null)
                closeBtn.Click += closeBtn_Click;
        }
        #endregion

        #region 事件处理
        void CalloutWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (PointX > 0 && PointY > 0)
            {
                Left = PointX - Width / 2 + Width * 14 / 130;
                Top = PointY - Height;
            }
        }
        void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
