using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp8_cookiee.Utils;

namespace WebApp8_cookiee.Models
{
    public record class StudentModel(string Id, string Password, string Grade="9", 
        string Department = "Phys");

   

  
    


    //public static class Peoples
    //{
    //    private static uint id = 0;
    //    public static uint Increment() => ++id;
    //    public static List<People> peoples = new List<People>();
    //}


    public static class Students
    { 
   
      public static List<StudentModel> students =
            new List<StudentModel>() ;
    
    }
     


}
