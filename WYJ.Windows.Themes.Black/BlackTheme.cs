using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using WYJ.Core.Interfaces;

namespace WYJ.Windows.Themes.Black
{
    [Export(typeof(ITheme))]
    public class BlackTheme : ITheme
    {
        public BlackTheme()
        {
            ThemeName = "Black";
        }
        public string ThemeName { get; set; }
        public Uri GetResourceUri()
        {
            return new Uri("/WYJ.Windows.Themes.Black;component/Themes/Generic.xaml", UriKind.Relative);
        }
    }
}
