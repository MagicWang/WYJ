using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYJ.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetProperty(this object obj, string name)
        {
            var p = obj.GetType().GetProperty(name);
            if (p != null)
                return p.GetValue(obj, null);
            return null;
        }
        public static void SetProperty(this object obj, string name, object value)
        {
            var p = obj.GetType().GetProperty(name);
            if (p != null)
                p.SetValue(obj, value, null);
        }
    }
}
