namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class NatureQuestionConfiguration : IEntityTypeConfiguration<NatureQuestion>
	{
		public void Configure(EntityTypeBuilder<NatureQuestion> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Libelle)
				.HasMaxLength(200);

			builder
				.HasMany(x => x.InfoSanteClient)
				.WithOne(y => y.NatureQuestion)
				.HasForeignKey(x => x.NatureQuestionId);

			builder
				.ToTable("NatureQuestions");
		}
	}
}
