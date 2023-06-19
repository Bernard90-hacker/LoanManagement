namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
	{
		public void Configure(EntityTypeBuilder<Utilisateur> builder)
		{
			builder
				.HasKey(x => x.Id);

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
				.Property(x => x.IsSuperAdmin)
				.IsRequired();

			builder
				.Property(x => x.IsAdmin)
				.IsRequired();

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateDesactivation)
				.IsRequired();

			builder
				.Property(x => x.DateExpirationCompte)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Statut)
				.IsRequired();

			builder
				.HasOne(x => x.Employe)
				.WithOne(y => y.User)
				.HasForeignKey<Employe>(y => y.UserId);

			builder
				.ToTable("Utilisateurs");
		}
	}
}
