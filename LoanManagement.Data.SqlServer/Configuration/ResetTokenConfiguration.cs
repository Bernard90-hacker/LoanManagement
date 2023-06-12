namespace LoanManagement.Data.SqlServer.Configuration
{
	public class ResetTokenConfiguration : IEntityTypeConfiguration<ResetToken>
	{
		public void Configure(EntityTypeBuilder<ResetToken> builder)
		{
			builder
				.HasKey(u => u.Email);

			builder
				.Property(r => r.Token)
				.IsRequired();

			builder
				.ToTable("ResetTokens");
		}
	}
}
