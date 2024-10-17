using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using WebApp9_cookiee_ef.Models;

namespace WebApp9_cookiee_ef.Entities
{
    public class UniversityDb:DbContext
    {

        public UniversityDb(DbContextOptions opt):base(opt) 
        {
            Database.EnsureCreated();
        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Role> Toles { get; set; }

    }
}
