using WebApp8_cookiee.Models;

namespace WebApp8_cookiee.Models
{
    public static class Resources
    {

        private static int id = 0;
        public static int Increment() => ++id;

        public static List<Role> Roles = new List<Role>() {
    new Role{Id=1,Name="Abitura"},
    new Role{Id=2,Name="Student"},
    new Role{Id=3,Name="Prepod"},
    new Role{Id=0,Name="Admin"}
    };

        public static List<People> Peoples = new List<People>
    {
     new People{Id = Resources.Increment(), Email="kate@ikit.ru",
         BirthDate=new DateOnly(2000,1,1),
         Name = "Kate",
         Sname = "Sharlock",
         Role = Roles.First<Role>(r=>r.Id==2),
         Password = "111"
     },
      new People{Id = Resources.Increment(), Email="basil@ikit.ru",
         BirthDate=new DateOnly(2003,10,1),
         Name = "Василий",
         Sname = "Петров",
         Role = Roles.First<Role>(r=>r.Id==2),
         Password = "111"
     },
    };
    }
}