namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class ProfilConfiguration : IEntityTypeConfiguration<Profil>
	{
		public void Configure(EntityTypeBuilder<Profil> builder)
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
				.HasIndex(x => x.Code)
				.IsUnique();

			builder
				.Property(x => x.Code)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Description)
				.IsRequired();

			builder
				.Property(x => x.DateExpiration)
				.IsRequired();

			builder.Property(x => x.DateAjout)
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"))
				.IsRequired();

			builder
				.Property(x => x.DateModification)
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"));

			builder
				.HasOne(x => x.Habilitation)
				.WithOne(y => y.Profil)
				.HasForeignKey<HabilitationProfil>(y => y.ProfilId)
				.IsRequired();

			builder
				.ToTable("Profils");

		}
	}
}
