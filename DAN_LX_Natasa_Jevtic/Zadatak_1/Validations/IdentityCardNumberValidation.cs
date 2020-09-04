using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class IdentityCardNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            /// <summary>
            /// This method checks if user input for identity card number is valid and unique.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="cultureInfo"></param>
            /// <returns>ValidationResult.</returns>
            string number = value as string;
            if (number.Length == 9)
            {
                Employees employees = new Employees();
                List<tblEmployee> employeeList = employees.GetEmployees();
                var user = employeeList.Where(x => x.NumberOfIdentityCard == number).FirstOrDefault();
                //if exists user with forwarded identity card number, return false
                if (user != null)
                {
                    if (this.Wrapper != null)
                    {
                        //if user editing
                        if (this.Wrapper.OldIdCardNumber == user.NumberOfIdentityCard)
                        {
                            return new ValidationResult(true, null);
                        }
                        //if user creating
                        else
                        {
                            return new ValidationResult(false, "This identity card already exists.");
                        }
                    }
                    else
                    {
                        return new ValidationResult(false, "This identity card already exists.");
                    }
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            else
            {
                return new ValidationResult(false, "Number must contain 9 digits.");
            }
        }
        public Wrapper Wrapper { get; set; }
    }
}
