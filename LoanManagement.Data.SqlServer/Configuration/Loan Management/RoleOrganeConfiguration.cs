namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class RoleOrganeConfiguration : IEntityTypeConfiguration<RoleOrgane>
	{
		public void Configure(EntityTypeBuilder<RoleOrgane> builder)
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
				.Property(x => x.DureeTraitement);

			builder
				.Property(x => x.OrganeDecisionId)
				.IsRequired();

			builder
				.ToTable("RoleOrganes");

		}
	}
}
