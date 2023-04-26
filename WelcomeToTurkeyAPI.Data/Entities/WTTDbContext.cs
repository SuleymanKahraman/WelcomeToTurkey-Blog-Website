using WelcomeToTurkeyAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WelcomeToTurkeyAPI.Data.Entities
{
    public class WTTDbContext: DbContext
    {
        public WTTDbContext(DbContextOptions opt) : base(opt) { }
        

    }
}
