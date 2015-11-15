using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WYJ.Windows.Controls
{
    [TemplatePart(Name = PART_OutBorder, Type = typeof(Border))]
    [TemplatePart(Name = PART_Header, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PART_Close, Type = typeof(Button))]
    [TemplatePart(Name = PART_ContentPresenter, Type = typeof(ContentPresenter))]
    public class NavigationFrame : Frame
    {
        #region 私有字段
        private const string PART_OutBorder = "PART_OutBorder";
        private const string PART_Header = "PART_Header";
        private const string PART_Close = "PART_Close";
        private const string PART_ContentPresenter = "PART_ContentPresenter";
        private Border _outBorder;
        private ContentPresenter _headerPresenter;
        private Button _closeBtn;
        private ContentPresenter _contentPresenter;
        private bool _allowDirectNavigation;
        private NavigatingCancelEventArgs _navArgs;
        private object root;
        private System.Collections.Generic.Dictionary<Uri, object> _instances;
        #endregion

        #region 构造函数
        static NavigationFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationFrame), new FrameworkPropertyMetadata(typeof(NavigationFrame)));
        }
        public NavigationFrame()
            : base()
        {
            _instances = new Dictionary<Uri, object>();
            Navigating += OnNavigating;
            Navigated += OnNavigated;
        }
        #endregion

        #region 依赖属性
        /// <summary>
        /// 是否显示导航标题栏,默认为false
        /// </summary>
        public bool ShowsNavigationUI
        {
            get { return (bool)GetValue(ShowsNavigationUIProperty); }
            set { SetValue(ShowsNavigationUIProperty, value); }
        }
        public static readonly DependencyProperty ShowsNavigationUIProperty =
            DependencyProperty.Register("ShowsNavigationUI", typeof(bool), typeof(NavigationFrame), new PropertyMetadata(false));
        /// <summary>
        /// 返回模式，默认为BackNavigationMode.PreviousScreen
        /// </summary>
        public BackNavigationMode BackNavigationMode
        {
            get { return (BackNavigationMode)GetValue(BackNavigationModeProperty); }
            set { SetValue(BackNavigationModeProperty, value); }
        }
        public static readonly DependencyProperty BackNavigationModeProperty =
            DependencyProperty.Register("BackNavigationMode", typeof(BackNavigationMode), typeof(NavigationFrame), new PropertyMetadata(BackNavigationMode.PreviousScreen));
        /// <summary>
        /// 缓存模式,默认为NavigationCacheMode.Enabled
        /// </summary>
        public NavigationCacheMode NavigationCacheMode
        {
            get { return (NavigationCacheMode)GetValue(NavigationCacheModeProperty); }
            set { SetValue(NavigationCacheModeProperty, value); }
        }
        public static readonly DependencyProperty NavigationCacheModeProperty =
            DependencyProperty.Register("NavigationCacheMode", typeof(NavigationCacheMode), typeof(NavigationFrame), new PropertyMetadata(NavigationCacheMode.Enabled));
        /// <summary>
        /// 缓存最大值，默认为10
        /// </summary>
        public int NavigationCacheMaxSize
        {
            get { return (int)GetValue(NavigationCacheMaxSizeProperty); }
            set { SetValue(NavigationCacheMaxSizeProperty, value); }
        }
        public static readonly DependencyProperty NavigationCacheMaxSizeProperty =
            DependencyProperty.Register("NavigationCacheMaxSize", typeof(int), typeof(NavigationFrame), new PropertyMetadata(10));
        /// <summary>
        /// 标题
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(NavigationFrame), new PropertyMetadata(null));
        /// <summary>
        /// 标题数据模板
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(NavigationFrame), new PropertyMetadata(null));
        /// <summary>
        /// 动画类型，默认为Fade
        /// </summary>
        public AnimationType AnimationType
        {
            get { return (AnimationType)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register("AnimationType", typeof(AnimationType), typeof(NavigationFrame), new PropertyMetadata(AnimationType.Fade));
        #endregion

        #region 重写方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _outBorder = Template.FindName(PART_OutBorder, this) as Border;
            _headerPresenter = Template.FindName(PART_Header, this) as ContentPresenter;
            _closeBtn = Template.FindName(PART_Close, this) as Button;
            if (_closeBtn != null)
                _closeBtn.Click += CloseBtn_Click;
            _contentPresenter = Template.FindName(PART_ContentPresenter, this) as ContentPresenter;
        }
        #endregion

        #region 事件处理方法
        protected virtual void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Content == null)
                return;
            if (NavigationCacheMode == NavigationCacheMode.Enabled)
            {
                if (e.Uri != null && !_instances.ContainsKey(e.Uri))
                    _instances.Add(e.Uri, e.Content);
            }
            else if (NavigationCacheMode == NavigationCacheMode.Required)
            {
                if (e.Uri != null && !_instances.ContainsKey(e.Uri))
                    _instances.Add(e.Uri, e.Content);
            }
            if (e.Content is Page)
            {
                Page page = e.Content as Page;
                Header = page.Title;
            }
            if (root == null)
            {
                root = e.Content;
            }
        }
        protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New && e.Uri != null && _instances.ContainsKey(e.Uri))
            {
                Navigate(_instances[e.Uri]);
                e.Cancel = true;
                return;
            }
            if (Content != null && !_allowDirectNavigation && _contentPresenter != null && AnimationType != Controls.AnimationType.None)
            {
                e.Cancel = true;
                _navArgs = e;
                _contentPresenter.IsHitTestVisible = false;
                var storyboardOut = (Template.Resources[string.Format("{0}Out", AnimationType)] as Storyboard).Clone();
                storyboardOut.Completed += storyboardOut_Completed;
                storyboardOut.Begin(_outBorder);
            }
            _allowDirectNavigation = false;
        }
        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (root != null && Content != root)
                Navigate(root);
        }
        void storyboardOut_Completed(object sender, EventArgs e)
        {
            if (_contentPresenter != null)
            {
                _contentPresenter.IsHitTestVisible = true;

                _allowDirectNavigation = true;
                switch (_navArgs.NavigationMode)
                {
                    case NavigationMode.New:
                        if (_navArgs.Uri == null)
                        {
                            NavigationService.Navigate(_navArgs.Content);
                        }
                        else
                        {
                            NavigationService.Navigate(_navArgs.Uri);
                        }
                        break;

                    case NavigationMode.Back:
                        NavigationService.GoBack();
                        break;

                    case NavigationMode.Forward:
                        NavigationService.GoForward();
                        break;

                    case NavigationMode.Refresh:
                        NavigationService.Refresh();
                        break;
                }
                if (AnimationType != Controls.AnimationType.None)
                {
                    var storyboardIn = Template.Resources[string.Format("{0}In", AnimationType)] as Storyboard;
                    storyboardIn.Begin(_outBorder);
                }
            }
        }
        #endregion
    }
    public enum AnimationType
    {
        /// <summary>
        /// 无动画
        /// </summary>
        None,
        /// <summary>
        /// 淡入淡出
        /// </summary>
        Fade,
        /// <summary>
        /// 滑动
        /// </summary>
        Slide,
        /// <summary>
        /// 伸缩
        /// </summary>
        Scale,
        /// <summary>
        /// 翻转
        /// </summary>
        Flip,
        /// <summary>
        /// 自旋
        /// </summary>
        Spin,
    }
    public enum BackNavigationMode
    {
        /// <summary>
        /// 前一页
        /// </summary>
        PreviousScreen,
        /// <summary>
        /// 根
        /// </summary>
        Root
    }
    public enum NavigationCacheMode
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
        /// <summary>
        /// 启用直到达到缓存最大值
        /// </summary>
        Enabled,
        /// <summary>
        /// 启用，忽略NavigationCacheMaxSize
        /// </summary>
        Required
    }
}
