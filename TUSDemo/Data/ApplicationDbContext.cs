using Microsoft.EntityFrameworkCore;
using TUSDemo.Models;

namespace TUSDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<StoredFile> Files { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
