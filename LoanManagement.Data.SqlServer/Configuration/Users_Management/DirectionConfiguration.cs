namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class DirectionConfiguration : IEntityTypeConfiguration<Direction>
	{
		public void Configure(EntityTypeBuilder<Direction> builder)
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
				.HasMaxLength(50);

			builder
				.ToTable("Directions");
		}
	}
}
