using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Storage;
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

        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
