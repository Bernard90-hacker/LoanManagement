using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LoanManagement.Data.SqlServer
{
    public class LoanManagementDbContext : DbContext
    {
        public LoanManagementDbContext(DbContextOptions<LoanManagementDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new ApplicationConfiguration());

			builder
				.ApplyConfiguration(new DepartementConfiguration());

			builder
				.ApplyConfiguration(new DirectionConfiguration());

			builder
				.ApplyConfiguration(new EmployeConfiguration());

			builder
				.ApplyConfiguration(new HabilitationProfilConfiguration());

			builder
				.ApplyConfiguration(new JournalConfiguration());

			builder
				.ApplyConfiguration(new MenuConfiguration());

			builder
				.ApplyConfiguration(new MotDePasseConfiguration());

			builder
				.ApplyConfiguration(new ParamMotDePasseConfiguration());

			builder
				.ApplyConfiguration(new ApplicationConfiguration());

			builder
				.ApplyConfiguration(new ProfilConfiguration());

			builder
				.ApplyConfiguration(new TypeJournalConfiguration());

			builder
				.ApplyConfiguration(new UtilisateurConfiguration());

			foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
			}
				
		}



        public DbSet<Application> Applications { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<HabilitationProfil> HabilitationProfils { get; set; }
        public DbSet<Journal> Journaux { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MotDePasse> MotDePasses { get; set; }
        public DbSet<ParamMotDePasse> ParamMotDePasses { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<TypeJournal> TypeJournaux { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}