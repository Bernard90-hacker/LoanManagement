namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class DeroulementConfiguration : IEntityTypeConfiguration<Deroulement>
	{
		public void Configure(EntityTypeBuilder<Deroulement> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Plancher)
				.IsRequired();

			builder
				.Property(x => x.Plafond)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired();

			builder
				.Property(x => x.NiveauInstance)
				.IsRequired();

			builder
				.Property(x => x.TypePretId)
				.IsRequired();

			builder
				.HasMany(x => x.Etapes)
				.WithOne(y => y.Deroulement)
				.HasForeignKey(y => y.DeroulementId);

			builder
				.ToTable("Deroulements");
		}
	}
}
