namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class OrganeDecisionConfiguration : IEntityTypeConfiguration<OrganeDecision>
	{
		public void Configure(EntityTypeBuilder<OrganeDecision> builder)
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
				.HasMany(x => x.Roles)
				.WithOne(y => y.OrganeDecision)
				.HasForeignKey(x => x.OrganeDecisionId);

			builder
				.ToTable("OrganeDecisions");
		}
	}
}
