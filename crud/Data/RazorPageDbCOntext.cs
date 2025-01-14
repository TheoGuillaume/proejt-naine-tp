using Crud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class RazorPageDbContext : DbContext
    {
        public RazorPageDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Poste> Poste {get; set;}
        public DbSet<Paye> Paye {get; set;}
        public DbSet<Mois> Mois {get; set;}
        public DbSet<Semaine> Semaine {get; set;}

         public DbSet<User> User {get; set;}

    }
}