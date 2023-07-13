namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class StatutMaritalConfiguration : IEntityTypeConfiguration<StatutMarital>
	{
		public void Configure(EntityTypeBuilder<StatutMarital> builder)
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
