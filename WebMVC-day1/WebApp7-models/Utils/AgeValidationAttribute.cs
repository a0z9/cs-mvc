using System.ComponentModel.DataAnnotations;

namespace WebApp7_models.Utils
{
    public class AgeValidationAttribute : ValidationAttribute
    {
        private int minAge, maxAge;

        public AgeValidationAttribute(int minAge, int maxAge)
        {
            this.minAge = minAge;
            this.maxAge = maxAge;
        }


        public override bool IsValid(object? value)
        {
            DateOnly? birthDay = value as DateOnly?;
            DateOnly? today = DateOnly.FromDateTime(DateTime.Now);
            int diff = today.Value.Year - birthDay.Value.Year;

            return diff >= minAge && diff <= maxAge;
            //return true;//base.IsValid(value);
        }

    }
}
