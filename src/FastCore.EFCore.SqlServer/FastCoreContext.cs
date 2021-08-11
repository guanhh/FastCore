using FastCore.Model;
using Microsoft.EntityFrameworkCore;

namespace FastCore.EFCore.SqlServer
{
    public class FastCoreContext : DbContext
    {
        public FastCoreContext(DbContextOptions<FastCoreContext> options)
      : base(options)
        {
        }

        public DbSet<FastUser> FastUsers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
