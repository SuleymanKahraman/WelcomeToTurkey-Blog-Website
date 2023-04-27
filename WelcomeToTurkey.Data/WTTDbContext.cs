using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WelcomeToTurkeyAPI.Data.Entities;

namespace WelcomeToTurkeyAPI.Data
{
    public class WTTDbContext:DbContext
    {

        public WTTDbContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        
            
       
    }
}
