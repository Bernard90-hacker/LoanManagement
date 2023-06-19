namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class ProfilConfiguration : IEntityTypeConfiguration<Profil>
	{
		public void Configure(EntityTypeBuilder<Profil> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

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

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateModification);

			builder
				.HasMany(x => x.Habilitations)
				.WithOne(y => y.Profil)
				.HasForeignKey(y => y.ProfilId)
				.IsRequired();

			builder
				.ToTable("Profils");

		}
	}
}
