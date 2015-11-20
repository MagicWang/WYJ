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
    /// Nullable转换为Visibility,对象非Null时为Visibility.Visible
    /// </summary>
    public class NullableToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return (this.Invert ? Visibility.Visible : Visibility.Collapsed);
            }
            return (this.Invert ? Visibility.Collapsed : Visibility.Visible);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public bool Invert { get; set; }
    }
}
