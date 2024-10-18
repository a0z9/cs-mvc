using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApp9_cookiee_ef.Utils;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp9_cookiee_ef.Models
{
    [Index(nameof(Email), IsUnique = true)]
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


        public static bool isAdmin() => false;   

        [BindNever]
        public int Id { get; set; } //id

        public const int MAX_AGE = 100, MIN_AGE = 16;

        private static DateOnly today = DateOnly.FromDateTime(DateTime.Now);
     
        public static readonly DateOnly OLD = today.AddYears(-MAX_AGE),
                               YOUNG = today.AddYears(-MIN_AGE);
        public const int NameSize = 15;
        public const int SNameSize = 20;
        
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
        public string Name { get; set; }

        [BindRequired]
        [Required]
        [NameValidation(SNameSize, ErrorMessage = "Фамилия слишком длинная, сократите..")]
        public string Sname { get; set; }

        [BindNever]
        [Column(TypeName ="varchar(120)")]
        public string? Department { get; set; }

        [BindRequired]
        [Required]
        [Remote(action: "AgeCheck", controller: "Home", ErrorMessage = "Неверный возраст.")]
        public DateOnly BirthDate { get; set; }

        [BindNever]
        public Guid? InternalId { get; set; }
        
        [BindNever]
        public string? Password { get; set; }

    }
}
