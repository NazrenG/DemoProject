using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Data
{
    public class DemoDb:DbContext
    {
        public DemoDb(DbContextOptions<DemoDb> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
