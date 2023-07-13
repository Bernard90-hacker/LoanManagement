namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class PeriodicitePaiementConfiguration : IEntityTypeConfiguration<PeriodicitePaiement>
	{
		public void Configure(EntityTypeBuilder<PeriodicitePaiement> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Libelle)
				.IsRequired();

			builder
				.ToTable("PeriodicitePaiements");
		}
	}
}
