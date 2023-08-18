using DemoDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAl.Contexts
{
    public class MVCContext:IdentityDbContext<ApplicationUser>
    {
        public MVCContext(DbContextOptions<MVCContext>options):base(options)
        {

        }
        public DbSet<Empployee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusinessLine> BusinessLines { get; set; }
    }
}
