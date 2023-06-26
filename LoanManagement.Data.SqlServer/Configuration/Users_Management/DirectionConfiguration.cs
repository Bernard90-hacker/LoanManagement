namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class DirectionConfiguration : IEntityTypeConfiguration<Direction>
	{
		public void Configure(EntityTypeBuilder<Direction> builder)
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
				.HasMaxLength(100);

			builder
				.ToTable("Directions");
		}
	}
}
