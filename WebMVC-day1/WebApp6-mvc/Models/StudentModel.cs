using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp6_mvc.Models
{
    public record class StudentModel(string Id, string Password, string Grade="9", 
        string Department = "Phys");


    public class Student {

        [BindRequired]
        public int Id { get; set; } //email

        [BindRequired]
        public string Name { get; set; }

        [BindRequired]
        public string Sname { get; set; }

        [BindNever]
        public string? Department { get; set; }
        [BindNever]
        public DateTime ExamDate {  get; set; }
        [BindNever]
        public Guid? InternalId { get; set; }
        [BindNever]
        public double? Grade { get; set; }
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
