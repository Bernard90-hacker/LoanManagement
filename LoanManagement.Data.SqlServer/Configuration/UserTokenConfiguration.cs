namespace LoanManagement.Data.SqlServer.Configuration
{
	public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.UseIdentityColumn();

			builder
				.Property(u => u.UserId);

			builder
				.Property(u => u.CreatedAt)
				.IsRequired()
				.HasDefaultValue(DateTime.Now);
			

			builder
				.Property(u => u.ExpiredAt)
				.IsRequired();

			builder
				.Property(u => u.Token)
				.IsRequired();

			builder
				.ToTable("UserTokens");
		}
	}
}
