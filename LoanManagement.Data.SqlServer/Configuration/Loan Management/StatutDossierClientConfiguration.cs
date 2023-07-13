namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class StatutDossierClientConfiguration : IEntityTypeConfiguration<StatutDossierClient>
	{
		public void Configure(EntityTypeBuilder<StatutDossierClient> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Libelle)
				.IsRequired();
		}
	}
}
