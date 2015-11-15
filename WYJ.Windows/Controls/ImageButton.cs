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
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }
        /// <summary>
        /// 图片资源
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 鼠标进入时显示图片
        /// </summary>
        public ImageSource MouseOverImageSource
        {
            get { return (ImageSource)GetValue(MouseOverImageSourceProperty); }
            set { SetValue(MouseOverImageSourceProperty, value); }
        }
        public static readonly DependencyProperty MouseOverImageSourceProperty =
            DependencyProperty.Register("MouseOverImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 鼠标按下时显示图片
        /// </summary>
        public ImageSource PressedImageSource
        {
            get { return (ImageSource)GetValue(PressedImageSourceProperty); }
            set { SetValue(PressedImageSourceProperty, value); }
        }
        public static readonly DependencyProperty PressedImageSourceProperty =
            DependencyProperty.Register("PressedImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 鼠标进入时的画刷
        /// </summary>
        public Brush MouseOverBrush
        {
            get { return (Brush)GetValue(MouseOverBrushProperty); }
            set { SetValue(MouseOverBrushProperty, value); }
        }
        public static readonly DependencyProperty MouseOverBrushProperty =
            DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 鼠标按下时的画刷
        /// </summary>
        public Brush PressedBrush
        {
            get { return (Brush)GetValue(PressedBrushProperty); }
            set { SetValue(PressedBrushProperty, value); }
        }
        public static readonly DependencyProperty PressedBrushProperty =
            DependencyProperty.Register("PressedBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
        /// <summary>
        /// 图片与文字的方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ImageButton), new PropertyMetadata(Orientation.Horizontal));
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageButton), new PropertyMetadata(16d));
        /// <summary>
        /// 图片高度
        /// </summary>
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageButton), new PropertyMetadata(16d));
    }
}
