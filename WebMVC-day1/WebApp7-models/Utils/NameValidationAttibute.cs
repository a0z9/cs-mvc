using System.ComponentModel.DataAnnotations;

namespace WebApp7_models.Utils
{
    public class NameValidationAttribute : ValidationAttribute
    {
        private int max;

        public NameValidationAttribute(int max)
        {
            this.max = max;
            
        }


        public override bool IsValid(object? value)
        {
            string Name = value as string;
          
            return Name.Length <= max;
         
        }

    }
}
