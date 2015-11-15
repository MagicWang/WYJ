using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using WYJ.TerraExplorer.Windows.Forms;

namespace WYJ.TerraExplorer.Windows
{
    public class Terra3DViewEx : ContentControl
    {
        public Terra3DViewEx()
        {
            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = new TerraControlEx();
            Content = host;
        }
    }
}
