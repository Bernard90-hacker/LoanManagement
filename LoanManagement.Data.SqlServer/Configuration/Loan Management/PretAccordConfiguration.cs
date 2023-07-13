namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class PretAccordConfiguration : IEntityTypeConfiguration<PretAccord>
	{
		public void Configure(EntityTypeBuilder<PretAccord> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.MontantPret)
				.IsRequired();

			builder
				.Property(x => x.DateDepartRetraite) 
				.IsRequired();

			builder
				.Property(x => x.DatePremiereEcheance)
				.IsRequired();

			builder
				.Property(x => x.DateDerniereEcheance)
				.IsRequired();

			builder
				.Property(x => x.MontantPrime)
				.IsRequired();

			builder
				.Property(x => x.Surprime)
				.IsRequired();

			builder
				.Property(x => x.PrimeTotale)
				.IsRequired();

			builder
				.Property(x => x.SalaireNetMensuel)
				.IsRequired();

			builder
				.Property(x => x.QuotiteCessible)
				.IsRequired();

			builder
				.Property(x => x.TauxEngagement)
				.IsRequired();

			builder
				.Property(x => x.TypePretId)
				.IsRequired();

			builder
				.Property(x => x.PeriodicitePaiementId)
				.IsRequired();

			builder
				.HasOne(x => x.PeriodicitePaiement)
				.WithMany(y => y.PretAccords)
				.HasForeignKey(x => x.PeriodicitePaiementId);

			builder
				.ToTable("PretAccords");
		}
	}
}
