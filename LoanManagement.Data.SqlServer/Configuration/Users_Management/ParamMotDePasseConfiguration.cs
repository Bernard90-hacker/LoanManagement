namespace LoanManagement.Data.SqlServer.Configuration.Users_Management
{
	public class ParamMotDePasseConfiguration : IEntityTypeConfiguration<ParamMotDePasse>
	{
		public void Configure(EntityTypeBuilder<ParamMotDePasse> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.IncludeDigits)
				.IsRequired();

			builder
				.Property(x => x.IncludeLowerCase)
				.IsRequired();

			builder
				.Property(x => x.IncludeUpperCase)
				.IsRequired();

			builder
				.Property(x => x.IncludeSpecialCharacters)
				.IsRequired();

			builder
				.Property(x => x.ExcludeUsername)
				.IsRequired();

			builder
				.Property(x => x.Taille)
				.IsRequired();

			builder
				.Property(x => x.DelaiExpiration)
				.IsRequired();

			builder
				.ToTable("ParamMotDePasses");

		}
	}
}
