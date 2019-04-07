using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using static MVC_Project.Models.Company;
using static MVC_Project.Models.Divident;
namespace MVC_Project.DataAccess
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Divident> Divident { get; set; }
        public DbSet<MVC_Project.Models.Price> Price { get; set; }
        public DbSet<MVC_Project.Models.Largest_Trade> Largest_Trade { get; set; }
        public DbSet<MVC_Project.Models.Splits> Splits { get; set; }
        public DbSet<MVC_Project.Models.Previous> Previous { get; set; }
       
        
    }
}
