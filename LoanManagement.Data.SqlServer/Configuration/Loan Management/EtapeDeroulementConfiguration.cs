﻿namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class EtapeDeroulementConfiguration : IEntityTypeConfiguration<EtapeDeroulement>
	{
		public void Configure(EntityTypeBuilder<EtapeDeroulement> builder)
		{
			builder
				.HasIndex(x => x.Id);

			builder
				.Property(x => x.Id)
				.UseIdentityColumn();

			builder
				.Property(x => x.Etape)
				.IsRequired();

			builder
				.Property(x => x.DeroulementId)
				.IsRequired();

			builder
				.HasMany(x => x.Statuts)
				.WithOne(y => y.EtapeDeroulement)
				.HasForeignKey(y => y.EtapeDeroulementId);


			builder
				.ToTable("EtapeDeroulements");
		}
	}
}
