using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using WYJ.TerraExplorer.Windows.Forms;

namespace WYJ.TerraExplorer.Windows
{
    public class TENavigationView : ContentControl
    {
        public TENavigationView()
        {
            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = new TENavigationControl();
            Content = host;
        }
    }
}
