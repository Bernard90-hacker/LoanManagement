namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class DossierClientConfiguration : IEntityTypeConfiguration<DossierClient>
	{
		public void Configure(EntityTypeBuilder<DossierClient> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.NumeroDossier)
				.IsRequired();

			builder
				.Property(x => x.Poids)
				.IsRequired();

			builder
			.Property(x => x.TensionArterielle)
			.IsRequired();

			builder
			.Property(x => x.Fumeur)
			.IsRequired();

			builder
			.Property(x => x.NbrCigarettes);

			builder
			.Property(x => x.BuveurOccasionnel)
			.IsRequired();

			builder
			.Property(x => x.BuveurRegulier);

			builder
			.Property(x => x.Distractions)
			.IsRequired();

			builder
			.Property(x => x.EstSportif)
			.IsRequired();

				builder
				.Property(x => x.EstInfirme)
				.IsRequired();

			builder
			.Property(x => x.CategorieSport);

			builder
			.Property(x => x.NatureInfirmite);

			builder
			.Property(x => x.DateSurvenance);

			builder
			.Property(x => x.StatutDossierClientId)
			.IsRequired();

			builder
			.Property(x => x.StatutMaritalId)
			.IsRequired();

			builder
			.Property(x => x.InfoSanteClientId)
			.IsRequired();

			builder
			.Property(x => x.PretAccordId)
			.IsRequired();

			builder
			.Property(x => x.ClientId)
			.IsRequired();

			builder
			.Property(x => x.EmployeurId)
			.IsRequired();

			builder
				.HasOne(x => x.StatutMarital)
				.WithMany(y => y.Dossiers)
				.HasForeignKey(x => x.StatutMaritalId);

			builder
				.HasOne(x => x.InfoSanteClient)
				.WithOne(y => y.Dossier)
				.HasForeignKey<DossierClient>(x => x.InfoSanteClientId);

			builder
				.HasOne(x => x.PretAccord)
				.WithOne(y => y.Dossier)
				.HasForeignKey<DossierClient>(x => x.PretAccordId);

			builder
				.HasOne(x => x.StatutDossierClient)
				.WithMany(y => y.Dossiers)
				.HasForeignKey(x => x.StatutDossierClientId);

			builder
				.ToTable("DossierClients");
		}
	}
}
