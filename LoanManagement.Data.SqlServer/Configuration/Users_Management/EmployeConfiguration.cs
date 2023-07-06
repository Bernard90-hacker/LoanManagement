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
				.Property(x => x.Matricule)
				.IsRequired();

			builder
				.HasIndex(x => x.Email)
				.IsUnique();

			builder
				.HasIndex(x => x.Matricule)
				.IsUnique();

			builder
				.Property(x => x.Nom)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Ignore(x => x.NomComplet);


			builder
				.Property(x => x.Prenoms)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(x => x.DateAjout)
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"))
				.IsRequired();

			builder.Property(x => x.DateModification)
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"))
				.IsRequired();

			builder
				.Property(x => x.UserId)
				.IsRequired();

			builder
				.HasOne(x => x.Departement)
				.WithMany(x => x.Employes)
				.HasForeignKey(x => x.DepartementId)
				.IsRequired();

			builder
				.ToTable("Employes");

		}
	}
}
