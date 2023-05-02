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

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }




    }
}
