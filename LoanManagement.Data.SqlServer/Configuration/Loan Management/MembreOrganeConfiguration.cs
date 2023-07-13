namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class MembreOrganeConfiguration : IEntityTypeConfiguration<MembreOrgane>
	{
		public void Configure(EntityTypeBuilder<MembreOrgane> builder)
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
				.Property(x => x.OrganeDecisionId)
				.IsRequired();

			builder
				.Property(x => x.UtilisateurId)
				.IsRequired();

			builder
				.HasOne(x => x.Utilisateur)
				.WithMany(y => y.Membres)
				.HasForeignKey(x => x.UtilisateurId);

			builder
				.HasOne(x => x.OrganeDecision)
				.WithMany(y => y.Membres)
				.HasForeignKey(x => x.OrganeDecisionId);

			builder
				.ToTable("MembreOrganes");
			
		}
	}
}
