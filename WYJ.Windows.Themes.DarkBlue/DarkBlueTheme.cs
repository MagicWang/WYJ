using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace WYJ.Windows.Themes.DarkBlue
{
    [Export(typeof(ITheme))]
    public class DarkBlueTheme : ITheme
    {
        public DarkBlueTheme()
        {
            ThemeName = "DarkBlue";
        }
        public string ThemeName { get; set; }
        public Uri GetResourceUri()
        {
            return new Uri("/WYJ.Windows.Themes.DarkBlue;component/Themes/Generic.xaml", UriKind.Relative);
        }
    }
}
