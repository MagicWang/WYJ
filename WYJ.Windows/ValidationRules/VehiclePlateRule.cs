using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WYJ.Windows.ValidationRules
{
    public class VehiclePlateRule : ValidationRule
    {
        public VehiclePlateRule()
        {

        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool flag = false;
            if (value != null)
                flag = Regex.IsMatch(value.ToString(), @"^[\u4e00-\u9fa5]{1}[a-zA-Z]{1}[a-zA-Z_0-9]{4}[a-zA-Z_0-9_\u4e00-\u9fa5]$|^[a-zA-Z]{2}\d{5}$");
            return new ValidationResult(flag, "车牌号码格式不正确!");
        }
    }
}
