namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class ClientConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.HasIndex(x => x.Indice)
				.IsUnique();

			builder
				.Property(x => x.Indice)
				.IsRequired();

			builder
				.Property(x => x.Nom)
				.HasMaxLength(30)
				.IsRequired();

			builder
				.Property(x => x.Prenoms)
				.HasMaxLength(30)
				.IsRequired();

			builder
				.Property(x => x.DateNaissance)
				.HasMaxLength(10)
				.IsRequired();


			builder
				.Property(x => x.Residence)
				.IsRequired();

			builder
				.Property(x => x.Ville)
				.IsRequired();

			builder
				.Property(x => x.Quartier)
				.IsRequired();

			builder
				.Property(x => x.Tel)
				.IsRequired();

			builder
				.Property(x => x.Profession)
				.IsRequired();

			builder
				.HasMany(x => x.DossierClients)
				.WithOne(y => y.Client)
				.HasForeignKey(y => y.ClientId);

			builder
				.HasMany(x => x.Comptes)
				.WithOne(y => y.Client)
				.HasForeignKey(y => y.ClientId);

			builder
				.ToTable("Clients");


		}
	}
}
