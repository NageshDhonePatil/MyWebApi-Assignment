using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Data
{
    public class MyKeyValueAPIDbContext : DbContext
    {
        public MyKeyValueAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MyKeyValue> MyKeyValues { get; set; }
    }
}
