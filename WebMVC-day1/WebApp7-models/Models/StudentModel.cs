using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp9_cookiee_ef.Utils;

namespace WebApp9_cookiee_ef.Models
{
    public record class StudentModel(string Id, string Password, string Grade="9", 
        string Department = "Phys");


    public static class Resourses {

    public static List<Role> Roles = new List<Role>() {
    new Role{Id=1,Name="Abitura"},
    new Role{Id=2,Name="Student"},
    new Role{Id=3,Name="Prepod"},
    new Role{Id=0,Name="Admin"}
    };

    public static List<People> Peoples = new List<People>();

    }
    

    public class Role { 
    public int Id { get; set; }
    public string Name { get; set; }
    }

    public class People {

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
        [NameValidation(SNameSize, ErrorMessage = "Фамилия слишком длинная, сократите..") ]
        public string Sname { get; set; }

        [BindNever]
        public string? Department { get; set; }

        [BindRequired]
        [Required]
        //[AgeValidation(maxAge:100, minAge:18, ErrorMessage = "Неверный возраст")]
        [Remote(action: "AgeCheck", controller: "Home", ErrorMessage = "Неверный возраст.")]
        public DateOnly BirthDate {  get; set; }

        [BindNever]
        public Guid? InternalId { get; set; }
        //[BindNever]
        //public double? Grade { get; set; }
        //...


    }


    public static class Peoples
    {
        private static uint id = 0;
        public static uint Increment() => ++id;
        public static List<People> peoples = new List<People>();
    }


    public static class Students
    { 
   
      public static List<StudentModel> students =
            new List<StudentModel>() { 
      new  StudentModel(Id:"Andrei",Password:"111","7"),
      new  StudentModel("Basil","111","9","Chem")


            };
    
    }
     


}
