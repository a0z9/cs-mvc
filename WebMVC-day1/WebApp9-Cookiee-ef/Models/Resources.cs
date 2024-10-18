using WebApp9_cookiee_ef.Models;

namespace WebApp9_cookiee_ef.Models
{
    public static class Resources
    {



        //private static int id = 0;
        //public static int Increment() => ++id;

        public static List<Role> Roles = new List<Role>();
        
   
        public static List<Role> InitRoles = new List<Role>() {
    new Role{Name="Abitura"},
    new Role{Name="Student"},
    new Role{Name="Prepod"},
    new Role{Name="Admin"}
    };

        private static List<string> emails = new List<string> {
        "kate@ikit.ru",
        "basil@ikit.ru",
        "jake@ikit.ru"
        };

    //    public static List<People> Peoples = new List<People>
    //{
    // new People{Id = Resources.Increment(), Email=emails[0],
    //     BirthDate=new DateOnly(2000,1,1),
    //     Name = "Kate",
    //     Sname = "Sharlock",
    //     Role = Roles.First<Role>(r=>r.Name=="Student"),
    //     Password = People.getPassHash($"{emails[0]}111"),
    //     InternalId = Guid.NewGuid()
    // },
    //  new People{Id = Resources.Increment(), Email=emails[1],
    //     BirthDate=new DateOnly(2003,10,1),
    //     Name = "Василий",
    //     Sname = "Петров",
    //     Role = Roles.First<Role>(r=>r.Name=="Admin"),
    //     Password =  People.getPassHash($"{emails[1]}111"),
    //     InternalId = Guid.NewGuid()
    // },
    //  new People{Id = Resources.Increment(), Email=emails[2],
    //     BirthDate=new DateOnly(2003,10,1),
    //     Name = "Иван",
    //     Sname = "Смирнов",
    //     Role = Roles.First<Role>(r=>r.Name=="Prepod"),
    //     Password =  People.getPassHash($"{emails[2]}111"),
    //     InternalId = Guid.NewGuid()
    // }
    //};

        public static List<People> InitPeoples= new List<People>
    {
     new People{
         Email=emails[0],
         BirthDate=new DateOnly(2000,1,1),
         Name = "Kate",
         Sname = "Sharlock",
         Role = InitRoles.First<Role>(r=>r.Name=="Student"),
         Password = People.getPassHash($"{emails[0]}111"),
         InternalId = Guid.NewGuid()
     },
      new People{ 
         Email=emails[1],
         BirthDate=new DateOnly(2003,10,1),
         Name = "Василий",
         Sname = "Петров",
         Role = InitRoles.First<Role>(r=>r.Name=="Admin"),
         Password =  People.getPassHash($"{emails[1]}111"),
         InternalId = Guid.NewGuid()
     },
      new People{ 
         Email=emails[2],
         BirthDate=new DateOnly(2003,10,1),
         Name = "Иван",
         Sname = "Смирнов",
         Role = InitRoles.First<Role>(r=>r.Name=="Prepod"),
         Password =  People.getPassHash($"{emails[2]}111"),
         InternalId = Guid.NewGuid()
     }
    };

    }
}