namespace WebApp6_mvc.Models
{
    public record class StudentModel(string Id, string Password, string Grade="9", 
        string Department = "Phys");


    public static class Students
    { 
      public static List<StudentModel> students =
            new List<StudentModel>() { 
      new  StudentModel(Id:"Andrei",Password:"111","7"),
      new  StudentModel("Basil","111","9","Chem")


            };
    
    }
     


}
