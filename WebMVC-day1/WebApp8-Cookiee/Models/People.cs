using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApp8_cookiee.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;S
using System.Security.Cryptography;
using System.Text;

namespace WebApp8_cookiee.Models
{
    public class People
    {

        public static string getPassHash(string input) {
            string output = ";sldkf928tig;km;qi459ig;salfk;slmvz/v,.";
            using (var sha256 = SHA256.Create())
            {
                byte[] arr = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                output = BitConverter.ToString(arr).Replace("-", ""); 
            }
              return output;
        }


        public static bool isAdmin() {

            
            return false;
        }

            [BindNever]
        public int Id { get; set; } //id

        public const int MAX_AGE = 100, MIN_AGE = 16;

        private static DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        //private static DateOnly Old = today.AddYears(-MAX_AGE);
        //private static DateOnly Young = today.AddYears(-MAX_AGE);

        public static readonly DateOnly OLD = today.AddYears(-MAX_AGE),
                               YOUNG = today.AddYears(-MIN_AGE);
        public const int NameSize = 15;
        public const int SNameSize = 20;
        // public readonly static string SNameError = $"Фамилия слишком длинная {SNameSize}";

        [BindRequired]
        [Required(ErrorMessage = "Не указана электронная почта")]
        [RegularExpression(@"[a-zA-Z0-9._\-%+]+@[a-zA-Z0-9._\-%+]+\.[a-zA-Z]{2,}",
            ErrorMessage = "Неправильный адрес.")]
        [Remote(action: "EmailCheck", controller: "Home", ErrorMessage = "Адрес уже используется.")]
        public string Email { get; set; } //email


        [BindRequired]
        [Required]
        public Role Role { get; set; }

        [BindRequired]
        [Required]
        [NameValidation(NameSize, ErrorMessage = "Имя слишком длинное, сократите..")]
        // [StringLength(maximumLength:15, ErrorMessage = "Имя слишком длинное")]
        public string Name { get; set; }

        [BindRequired]
        [Required]
        [NameValidation(SNameSize, ErrorMessage = "Фамилия слишком длинная, сократите..")]
        public string Sname { get; set; }

        [BindNever]
        public string? Department { get; set; }

        [BindRequired]
        [Required]
        //[AgeValidation(maxAge:100, minAge:18, ErrorMessage = "Неверный возраст")]
        [Remote(action: "AgeCheck", controller: "Home", ErrorMessage = "Неверный возраст.")]
        public DateOnly BirthDate { get; set; }

        [BindNever]
        public Guid? InternalId { get; set; }
        //[BindNever]
        //public double? Grade { get; set; }
        //...
        [BindNever]
        public string? Password { get; set; }

    }
}
