namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class JournalConfiguration : IEntityTypeConfiguration<Journal>
	{
		public void Configure(EntityTypeBuilder<Journal> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Niveau)
				.IsRequired();

			builder
				.Property(x => x.IPAdress)
				.IsRequired();

			builder
				.Property(x => x.Machine)
				.IsRequired();

			builder
				.Property(x => x.OS)
				.IsRequired();

			builder
				.Property(x => x.Peripherique)
				.IsRequired();

			builder
				.Property(x => x.PageURL)
				.IsRequired();

			builder
				.Property(x => x.Entite)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired();

			builder
				.Property(x => x.MethodeHTTP)
				.IsRequired();

			builder
				.Property(x => x.DateOperation)
				.IsRequired();

			builder
				.Property(x => x.DateSysteme)
				.IsRequired();

			builder
				.Property(x => x.PreferenceURL)
				.IsRequired();

			builder
				.Property(x => x.Navigateur)
				.IsRequired();

			builder
				.Property(x => x.TypeJournalId)
				.IsRequired();

			builder
				.Property(x => x.UtilisateurId)
				.IsRequired();

			builder
				.HasOne(x => x.Utilisateur)
				.WithMany(x => x.Journaux)
				.HasForeignKey(x => x.UtilisateurId)
				.IsRequired();

			builder
				.HasOne(x => x.TypeJournal)
				.WithMany(x => x.Journaux)
				.HasForeignKey(x => x.TypeJournalId)
				.IsRequired();

			builder
				.ToTable("Journaux");
		}
	}
}
