using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMessenger.Validation
{
    public class DataValidation
    {
        ValidationContext context;
        List<ValidationResult> results;
        bool valid;
        string message;

        public DataValidation(object instance)
        {
            context = new ValidationContext(instance);
            results = new List<ValidationResult>();
            valid = Validator.TryValidateObject(instance, context, results, true);
        }

        public bool Validate()
        {
            if (!valid)
            {
                foreach (ValidationResult result in results)
                    message += result.ErrorMessage + "\n";
                MessageBox.Show(message);
                //return message;
            }
            return valid;
        }
    }
}