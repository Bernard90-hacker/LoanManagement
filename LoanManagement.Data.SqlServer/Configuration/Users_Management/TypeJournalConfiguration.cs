namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class TypeJournalConfiguration : IEntityTypeConfiguration<TypeJournal>
	{
		public void Configure(EntityTypeBuilder<TypeJournal> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.HasIndex(x => x.Code)
				.IsUnique();

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Code)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired()
				.HasMaxLength(20);

			builder
				.Property(x => x.Statut)
				.IsRequired();

			builder
				.ToTable("TypeJournaux");

		}
	}
}
