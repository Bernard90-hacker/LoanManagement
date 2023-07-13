namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class EmployeurConfiguration : IEntityTypeConfiguration<Employeur>
	{
		public void Configure(EntityTypeBuilder<Employeur> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.HasIndex(x => x.BoitePostale)
				.IsUnique();

			builder
				.HasIndex(x => x.Tel)
				.IsUnique();

			builder
				.Property(x => x.Nom)
				.IsRequired();

			builder
				.Property(x => x.BoitePostale)
				.IsRequired();

			builder
				.Property(x => x.Tel)
				.IsRequired();

			builder
				.Property(x => x.BoitePostale)
				.IsRequired();

			builder
				.ToTable("Employeurs");
		}
	}
}
