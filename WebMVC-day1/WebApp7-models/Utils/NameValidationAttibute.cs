using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApp9_cookiee_ef.Models;

namespace WebApp9_cookiee_ef.Utils
{
    public class NameValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private int max;

        public NameValidationAttribute(int max)
        {
            this.max = max;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "n-size", $"{max}");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-tolong", errorMessage);
        }

        private bool MergeAttribute(
        IDictionary<string, string> attributes,
        string key,
        string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }

       public override bool IsValid(object value)
        {
            string Name = value as string;

            return Name.Length <= max;
            

        }

    }



    //public class NameValidation2Attribute : Attribute, IModelValidator
    //{
    //    private int max;
    //    public string ErrorMessage { get; set; }

    //    public NameValidation2Attribute(int max)
    //    {
    //        this.max = max;
    //    }

    //    public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
    //    {

    //        People? p = (People?)context.Model;

    //        if (p.Name.Length <= max) return ValidationResult.Success;
    //        return new ValidationResult(ErrorMessage);

    //    }

    //}
}
