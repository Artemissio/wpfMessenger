using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMessenger.Validation
{
    public static class DataValidation
    {
        //public static bool Validate(object instance)
        //{
        //    ValidationContext context = new ValidationContext(instance);
        //    List<ValidationResult> results = new List<ValidationResult>();
        //    bool valid = Validator.TryValidateObject(instance, context, results, true);
        //    string message = string.Empty;

        //    if (!valid)
        //    {
        //        foreach (ValidationResult result in results)
        //            message += result.ErrorMessage + "\n";
        //        MessageBox.Show(message);
        //    }

        //    return valid;
        //}

        public static bool ValidateString(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length < 3 || value.Trim().Length > 15 ||
                !value.Any(char.IsLower) || value.Any(char.IsNumber))
                return false;
            return true;
        }

        public static bool ValidatePassword(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length < 8 || value.Trim().Length > 15 || !value.Any(char.IsUpper)
                || !value.Any(char.IsLower) || !value.Any(char.IsNumber))
                return false;

            return true;
        }

        public static bool ValidateConfirmation(string password, string confirm)
        {
            if(password != null && confirm != null)
                return password == confirm;

            return false;
        } 

        public static bool ValidateNumber(string value)
        {
            string reg = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";

            if(value != null)
                return Regex.IsMatch(value, reg);

            return false;
        }

        public static bool ValidateTitle(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length < 3 || value.Trim().Length > 30)
                return false;
            return true;
        }
    }
}