namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class MenuConfiguration : IEntityTypeConfiguration<Menu>
	{
		public void Configure(EntityTypeBuilder<Menu> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Logo);

			builder
				.Property(x => x.Code)
				.IsRequired();

			builder
				.Property(x => x.Libelle)
				.IsRequired();

			builder
				.Property(x => x.Position)
				.IsRequired();

			builder
				.Property(x => x.Description)
				.IsRequired();

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateModification);

			builder
				.Property(x => x.Code)
				.IsRequired();

			builder
				.Property(x => x.MenuId);

			builder
				.Property(x => x.ApplicationId);

			builder
				.Property(x => x.HabilitationProfilId)
				.IsRequired();

			builder
				.HasOne<Menu>()
				.WithMany(x => x.SousMenus);

			builder
				.HasOne<HabilitationProfil>()
				.WithMany(x => x.Menus);

			builder
				.HasOne<Application>()
				.WithMany(x => x.Menus);
		}
	}
}
