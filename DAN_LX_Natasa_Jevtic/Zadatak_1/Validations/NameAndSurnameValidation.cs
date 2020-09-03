using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class NameAndSurnameValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user name and surname.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string user = value as string;
            string[] nameAndSurname = user.Split();
            if (nameAndSurname.Length > 1 && nameAndSurname[1] != "")
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Please enter both.");
            }
        }
    }
}
