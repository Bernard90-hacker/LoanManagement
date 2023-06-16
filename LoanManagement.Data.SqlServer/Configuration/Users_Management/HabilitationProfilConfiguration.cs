namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class HabilitationProfilConfiguration : IEntityTypeConfiguration<HabilitationProfil>
	{
		public void Configure(EntityTypeBuilder<HabilitationProfil> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Insertion)
				.IsRequired();

			builder
				.Property(x => x.Edition)
				.IsRequired();

			builder
				.Property(x => x.Generation)
				.IsRequired();

			builder
				.Property(x => x.Modification)
				.IsRequired();

			builder
				.Property(x => x.Suppression)
				.IsRequired();

			builder
				.Property(x => x.DateAjout)
				.IsRequired();

			builder
				.Property(x => x.DateModification);

			builder
				.ToTable("HabilitationProfils");
		}
	}
}
