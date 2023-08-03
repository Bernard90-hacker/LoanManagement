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
			.Property(x => x.StatutMaritalId)
			.IsRequired();

			builder
			.Property(x => x.ContratTravail);

			builder
			.Property(x => x.AttestationTravail);

			builder
			.Property(x => x.PremierBulletinSalaire);

			builder
			.Property(x => x.DeuxiemeBulletinSalaire);

			builder
			.Property(x => x.TroisiemeBulletinSalaire);

			builder
			.Property(x => x.FactureProFormat)
            .HasDefaultValue(null);


            builder
			.Property(x => x.ClientId)
			.IsRequired();

			builder
				.HasOne(x => x.StatutMarital)
				.WithMany(y => y.Dossiers)
				.HasForeignKey(x => x.StatutMaritalId);

			builder
				.HasMany(x => x.InfoSanteClients)
				.WithOne(y => y.Dossier)
				.HasForeignKey(y => y.DossierClientId);

			builder
				.HasOne(x => x.PretAccord)
				.WithOne(y => y.Dossier)
				.HasForeignKey<PretAccord>(x => x.DossierClientId);

			builder
				.HasMany(x => x.Status)
				.WithOne(y => y.Dossier)
				.HasForeignKey(y => y.DossierClientId);

			builder
				.ToTable("DossierClients");
		}
	}
}
