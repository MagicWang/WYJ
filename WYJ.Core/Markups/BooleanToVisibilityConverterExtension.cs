using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WYJ.Core.Markups
{
    /// <summary>
    /// Boolean转换为Visibility
    /// </summary>
    public class BooleanToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;
            if ((parameter is string) && (((string) parameter) == "HiddenOnFalse"))
            {
                result = Visibility.Hidden;
            }
            if ((value != null) && ((bool) value))
            {
                return (this.Invert ? ((object) result) : ((object) 0));
            }
            return (this.Invert ? ((object) 0) : ((object) result));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility) value;
            if (visibility == Visibility.Collapsed)
            {
                return this.Invert;
            }
            return !this.Invert;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public bool Invert { get; set; }
    }
}
