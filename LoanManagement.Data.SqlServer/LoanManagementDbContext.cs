using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer
{
    public class LoanManagementDbContext : DbContext
    {
        public LoanManagementDbContext(DbContextOptions<LoanManagementDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder
                .ApplyConfiguration(new UserConfiguration());

            builder
                .ApplyConfiguration(new ResetTokenConfiguration());

            builder
                .ApplyConfiguration(new UserTokenConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
    }
}