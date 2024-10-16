using System.ComponentModel.DataAnnotations;

namespace OrganizationService.Validation
{
    public class EitherOneIsRequiredAttribute : CompareAttribute
    {
        public EitherOneIsRequiredAttribute(string otherProperty) : base(otherProperty)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherTypeInfo = validationContext.ObjectInstance.GetType().GetProperty(this.OtherProperty);
            var typeInfo = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);

            var otherPropertyValue = otherTypeInfo.GetValue(validationContext.ObjectInstance);
            var comparePropertyValue = typeInfo.GetValue(validationContext.ObjectInstance);

            if (otherPropertyValue == null && comparePropertyValue == null) {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
