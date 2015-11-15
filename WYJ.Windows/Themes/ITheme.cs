using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYJ.Windows.Themes
{
    public interface ITheme
    {
        string ThemeName { get; set; }
        Uri GetResourceUri();
    }
}
