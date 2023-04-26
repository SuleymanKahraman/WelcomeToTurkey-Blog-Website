using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WelcomeToTurkeyAPI.Data
{
    public class WTTDbContext:DbContext
    {

        public WTTDbContext(DbContextOptions opt) : base(opt) { }
        
            
       
    }
}
