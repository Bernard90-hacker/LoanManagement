namespace LoanManagement.Data.SqlServer.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.UseIdentityColumn();

			builder
				.Property(u => u.Id)
				.UseIdentityColumn();

			builder
				.Property(u => u.Email)
				.HasMaxLength(50)
				.HasJsonPropertyName("email")
				.IsRequired();

			builder
				.Property(u => u.Password)
				.IsRequired()
				.HasJsonPropertyName("password")
				.HasMaxLength(80);

			builder
				.Property(u => u.FirstName)
				.HasJsonPropertyName("first_name")
				.IsRequired()
				.HasMaxLength(20);

			builder
				.Property(u => u.LastName).IsRequired()
				.HasJsonPropertyName("last_name")
				.HasMaxLength(30);

			builder
				.ToTable("Users");
		}
	}
}
