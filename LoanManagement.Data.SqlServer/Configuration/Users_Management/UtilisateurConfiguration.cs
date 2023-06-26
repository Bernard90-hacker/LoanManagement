namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
	{
		public void Configure(EntityTypeBuilder<Utilisateur> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.HasIndex(x => x.Username)
				.IsUnique();

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Username)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.PasswordHash)
				.IsRequired();

			builder
				.Property(x => x.PasswordSalt)
				.IsRequired();

			builder
				.Property(x => x.RefreshToken)
				.IsRequired();

			builder
				.Property(x => x.RefreshTokenTime)
				.IsRequired();

			builder
				.Property(x => x.IsEditPassword)
				.IsRequired();

			builder
				.Property(x => x.ProfilId);

			builder
				.Property(x => x.IsSuperAdmin)
				.IsRequired();

			builder
				.Property(x => x.IsAdmin)
				.IsRequired();

			builder
				.Property(x => x.DateAjout)
				.IsRequired()
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"));

			builder
				.Property(x => x.DateDesactivation);

			builder
				.Property(x => x.DateModificationMotDePasse);

			builder
				.Property(x => x.DateExpirationCompte)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Statut);

			builder
				.HasOne(x => x.Employe)
				.WithOne(y => y.User)
				.HasForeignKey<Employe>(y => y.UserId);

			builder
				.HasOne(x => x.Profil)
				.WithMany(y => y.Utilisateurs)
				.HasForeignKey(x => x.ProfilId);

			builder
				.ToTable("Utilisateurs");
		}
	}
}
