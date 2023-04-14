using Humanizer;
using PERM_TASKS.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PERM.Models.Validation
{
    public class DateRange : ValidationAttribute
    {
        private readonly string _propertyName;

        public DateRange(string propertyName)
        {
            _propertyName = propertyName;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentValue = (DateTime)value;

            var model = (ModelBase)validationContext.ObjectInstance;

            if (!string.IsNullOrEmpty(_propertyName) && model != null)
            {
                Console.WriteLine("VALIDATING...");
                var property = model.GetType().GetProperties().FirstOrDefault(s => s.Name == _propertyName);
                var compareValue = (DateTime)property.GetValue(model, null); // date of issuesence

                if (compareValue > currentValue)
                    return new ValidationResult($"The field {validationContext.MemberName.Humanize(LetterCasing.Title)} must be greater than {property.Name.Humanize(LetterCasing.Title)}");
            }

            return ValidationResult.Success;
        }
    }
}
