namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class DepartementConfiguration : IEntityTypeConfiguration<Departement>
	{
		public void Configure(EntityTypeBuilder<Departement> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Code)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired()
				.HasMaxLength(50);

			builder
				.Property(x => x.DirectionId)
				.IsRequired();

			builder
				.HasOne(x => x.Direction)
				.WithMany(x => x.Departements)
				.HasForeignKey(x => x.DirectionId);

			builder
				.ToTable("Departements");

		}
	}
}
