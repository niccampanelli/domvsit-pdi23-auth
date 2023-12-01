using Microsoft.EntityFrameworkCore;
using Domain.Entities.User;

namespace Infrastructure.Setup
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshTokenEntity>()
                .HasOne(r => r.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshTokenEntity>(r => r.UserId);
        }
    }
}
