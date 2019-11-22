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
        public static bool ValidateString(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length >= 3 && value.Trim().Length <= 15 &&
                value.Any(char.IsLower) && !value.Any(char.IsNumber);
        }

        public static bool ValidatePassword(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length >= 8 && value.Trim().Length <= 15 && value.Any(char.IsUpper)
                && value.Any(char.IsLower) && value.Any(char.IsNumber);
        }

        public static bool ValidateConfirmation(string password, string confirm)
        {
            return password != null && confirm != null ? password == confirm : false;
        }

        public static bool ValidateNumber(string value)
        {
            if (value == null || value.Length > 13)
                return false;

            string reg = @"\d{3}-\d{3}-\d{2}-\d{2}";
            return Regex.IsMatch(value, reg);
        }

        public static bool ValidateTitle(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length >= 3 && value.Trim().Length <= 30;
        }
    }
}