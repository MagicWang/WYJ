using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WYJ.Windows.Controls
{
    /// <summary>
    /// 再次点击取消选中状态的RadioButton
    /// </summary>
    public class UnCheckableRadioButton : ToggleButton
    {
        private static List<UnCheckableRadioButton> group;
        static UnCheckableRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UnCheckableRadioButton), new FrameworkPropertyMetadata(typeof(UnCheckableRadioButton)));
        }
        protected override void OnChecked(RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GroupName))
                group = VisualTreeHelper1.GetSiblingObjects(this).Except(new[] { this }).Where(l => l.GroupName == "").ToList();
            else
            {
                var root = VisualTreeHelper1.GetRoot(this);
                if (root != null)
                    group = VisualTreeHelper1.GetChildObjects<UnCheckableRadioButton>(root).Except(new[] { this }).Where(l => l.GroupName == this.GroupName).ToList();
            }
            if (group != null)
                group.ForEach(l => l.IsChecked = false);
            base.OnChecked(e);
        }
        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(UnCheckableRadioButton), new PropertyMetadata(""));
    }
}
