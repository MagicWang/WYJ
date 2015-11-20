using System;
using System.Collections;
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
    /// 集合对象转为Visibility,集合非空时为Visibility.Visible
    /// </summary>
    public class CollectionToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = Visibility.Collapsed;
            if ((parameter is string) && (((string)parameter) == "HiddenOnFalse"))
            {
                result = Visibility.Hidden;
            }
            if (value != null && value is IEnumerable)
            {
                var list = value as IEnumerable;
                if (list.GetEnumerator().MoveNext())
                    return (this.Invert ? ((object)result) : ((object)0));
            }
            return (this.Invert ? ((object)0) : ((object)result));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public bool Invert { get; set; }
    }
}
