using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.Options;
using Domain.Entities.User;

namespace Infrastructure.Setup
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString;
        public virtual DbSet<UserEntity> Users { get; set; }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options,
            IOptions<Secrets> secrets
        ) : base(options)
        {
            _connectionString = secrets.Value.DatabaseConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
