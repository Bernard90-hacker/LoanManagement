namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class EmployeConfiguration : IEntityTypeConfiguration<Employe>
	{
		public void Configure(EntityTypeBuilder<Employe> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Nom)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Prenoms)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(50);

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateModification)
				.IsRequired();

			builder
				.HasOne<Departement>()
				.WithMany(x => x.Employes)
				.IsRequired();

		}
	}
}
