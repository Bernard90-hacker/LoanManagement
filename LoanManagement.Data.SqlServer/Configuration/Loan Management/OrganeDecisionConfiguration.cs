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
				.Property(x => x.RoleOrganeId)
				.IsRequired();

			builder
				.HasOne(x => x.Role)
				.WithMany(y => y.OrganeDecisions)
				.HasForeignKey(x => x.RoleOrganeId);

			builder
				.ToTable("OrganeDecisions");
		}
	}
}
