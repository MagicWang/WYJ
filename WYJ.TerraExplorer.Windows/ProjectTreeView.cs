using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using WYJ.TerraExplorer.Windows.Forms;

namespace WYJ.TerraExplorer.Windows
{
    public class ProjectTreeView : ContentControl
    {
        public ProjectTreeView()
        {
            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = new ProjectTreeControl();
            Content = host;
        }
    }
}
