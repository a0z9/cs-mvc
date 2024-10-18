using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp9_cookiee_ef.Controllers;
using WebApp9_cookiee_ef.Models;

namespace WebApp9_cookiee_ef.Entities
{
    public class UniversityDb:DbContext
    {

        public UniversityDb(DbContextOptions opt, [FromServices] ILogger<HomeController> log):base(opt) 
        {
            log.LogInformation($"+++ db created: [{ContextId}]");
            Database.EnsureCreated();

           /*
            if (!Roles.Any() && !Peoples.Any())
            {
                Roles.AddRange(Resources.InitRoles);
                Peoples.AddRange(Resources.InitPeoples);
                SaveChanges();
            }*/

            Resources.Roles = Roles.ToList();
        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
