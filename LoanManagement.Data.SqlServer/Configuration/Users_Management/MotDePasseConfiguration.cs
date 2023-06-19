namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class MotDePasseConfiguration : IEntityTypeConfiguration<MotDePasse>
	{
		public void Configure(EntityTypeBuilder<MotDePasse> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.OldPasswordSalt)
				.IsRequired();

			builder
				.Property(x => x.OldPasswordSalt)
				.IsRequired();

			builder
				.Property(x => x.UtilisateurId)
				.IsRequired();

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.HasOne(x => x.Utilisateur)
				.WithMany(y => y.Passwords)
				.HasForeignKey(x => x.UtilisateurId)
				.IsRequired();

			builder
				.ToTable("MotDePasses");
		}
	}
}
