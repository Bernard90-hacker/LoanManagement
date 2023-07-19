namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class InfoSanteClientConfiguration : IEntityTypeConfiguration<InfoSanteClient>
	{
		public void Configure(EntityTypeBuilder<InfoSanteClient> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.ReponseBoolenne)
				.IsRequired();

			builder
				.Property(x => x.ReponsePrecision);

			builder
				.Property(x => x.DureeTraitement);

			builder
				.Property(x => x.PeriodeTraitement);

			builder
				.Property(x => x.LieuTraitement);

			builder
				.Property(x => x.NatureQuestionId)
				.IsRequired();

			builder
				.Property(x => x.DossierClientId)
				.IsRequired();

			builder
				.ToTable("InfoSanteClients");
		}
	}
}
