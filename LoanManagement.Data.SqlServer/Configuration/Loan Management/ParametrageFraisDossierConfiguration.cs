namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class ParametrageFraisDossierConfiguration : IEntityTypeConfiguration<ParametrageFraisDossier>
	{
		public void Configure(EntityTypeBuilder<ParametrageFraisDossier> builder)
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
				.Property(x => x.Plafond);

			builder
				.Property(x => x.PourcentageCommissionEngagement)
				.IsRequired();

			builder
				.Property(x => x.FraisFixe)
				.IsRequired();

			builder
				.Property(x => x.PourcentageTAF)
				.IsRequired();

			builder
				.ToTable("ParametrageFraisDossiers");
		}
	}
}
