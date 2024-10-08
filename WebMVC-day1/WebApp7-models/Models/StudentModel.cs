using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp7_models.Models
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

        [BindRequired]
        [Required]
        public string Email { get; set; } //email

        public Role Role { get; set; }

        [BindRequired]
        [Required]
        public string Name { get; set; }

        [BindRequired]
        [Required]
        public string Sname { get; set; }

        [BindNever]
        public string? Department { get; set; }

        [BindRequired]
        public DateOnly BirthDate {  get; set; }

        [BindNever]
        public Guid? InternalId { get; set; }
        //[BindNever]
        //public double? Grade { get; set; }
        //...


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
