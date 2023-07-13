namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class TypePretConfiguration : IEntityTypeConfiguration<TypePret>
	{
		public void Configure(EntityTypeBuilder<TypePret> builder)
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
				.HasMany(x => x.PretAccords)
				.WithOne(y => y.TypePret)
				.HasForeignKey(y => y.TypePretId);
		}
	}
}
