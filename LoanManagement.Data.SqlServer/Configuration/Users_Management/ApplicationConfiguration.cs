using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
	{
		public void Configure(EntityTypeBuilder<Application> builder)
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
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"))
				.IsRequired();

			builder
				.Property(x => x.DateModification)
				.HasDefaultValue(DateTime.Now.ToString("dd/MM/yyyy"))
				.IsRequired();

			builder
				.Property(x => x.ApplicationId)
				.HasDefaultValue(null);

			builder
				.ToTable("Applications");

		}
	}
}
