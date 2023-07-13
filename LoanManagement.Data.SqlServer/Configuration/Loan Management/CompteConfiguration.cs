namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class CompteConfiguration : IEntityTypeConfiguration<Compte>
	{
		public void Configure(EntityTypeBuilder<Compte> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.HasIndex(x => x.NumeroCompte)
				.IsUnique();

			builder
				.Property(x => x.NumeroCompte)
				.HasMaxLength(13)
				.IsRequired();


			builder
				.Property(x => x.Solde)
				.IsRequired()
				.HasDefaultValue(0);

			builder
				.Property(x => x.ClientId)
				.IsRequired();

			builder
				.ToTable("Comptes");
		}
	}
}
