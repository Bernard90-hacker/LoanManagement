namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
	{
		public void Configure(EntityTypeBuilder<Application> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Libelle)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Code)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Logo)
				.IsRequired();

			builder
				.Property(x => x.Description);

			builder
				.Property(x => x.Statut)
				.IsRequired();

			builder
				.Property(x => x.Version)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateModification)
				.IsRequired();

			builder
				.Property(x => x.ModuleId);

			builder
				.HasOne(x => x.Module)
				.WithMany(y => y.Modules)
				.HasForeignKey(x => x.ModuleId);

			builder
				.ToTable("Applications");

		}
	}
}
