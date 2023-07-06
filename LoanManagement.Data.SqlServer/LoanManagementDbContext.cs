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

			builder
				.Entity<TypeJournal>().HasData(
					new TypeJournal()
					{
						Id = 1,
						Code = "CONN",
						Libelle = "Connexion"
					},
					new TypeJournal()
					{
						Id = 2,
						Code = "DECONN",
						Libelle = "Déconnexion"
					},
					new TypeJournal()
					{
						Id = 3,
						Code = "UPDATE",
						Libelle = "Modification de données"
					},
					new TypeJournal()
					{
						Id = 4,
						Code = "DELETE",
						Libelle = "Suppression des données"
					},
					new TypeJournal()
					{
						Id = 5,
						Code = "ADD",
						Libelle = "Ajout de données"
					},
					new TypeJournal()
					{
						Id = 6,
						Code = "GETBYID",
						Libelle = "Rechercher un objet par son id"
					},
					new TypeJournal()
					{
						Id = 7,
						Code = "GETBYCODE",
						Libelle = "Rechercher un objet par son code"
					},
					new TypeJournal()
					{
						Id = 8,
						Code = "GET",
						Libelle = "Récupération de données"
					},
					new TypeJournal()
					{
						Id = 9,
						Code = "MNT",
						Libelle = "Montage de dossier crédit"
					}
				);

			builder
				.Entity<ParamMotDePasse>().HasData(

					new ParamMotDePasse()
					{
						Id = 1,
						IncludeDigits = true,
						IncludeLowerCase = true,
						IncludeSpecialCharacters = true,
						ExcludeUsername = true,
						IncludeUpperCase = true,
						DelaiExpiration = 6,
						Taille = 8,
					}
				);

			builder
				.Entity<Direction>().HasData(

					new Direction()
					{
						Id = 1,
						Code = "DSIOSI",
						Libelle = "Direction informatique"
					},
					new Direction()
					{
						Id = 2,
						Code = "DRC",
						Libelle = "Direction commerciale"
					},
					new Direction()
					{
						Id = 3,
						Code = "GGE",
						Libelle = "Direction gestion des engagements"
					}
				);

			builder
				.Entity<Departement>().HasData(

					new Departement()
					{
						Id = 1,
						Code = "ETUDEV",
						Libelle = "Etudes et développement",
						DirectionId = 1
					},
					new Departement()
					{
						Id = 2,
						Code = "EXPL",
						Libelle = "Exploitation",
						DirectionId = 1
					},
					new Departement()
					{
						Id = 3,
						Code = "ORG",
						Libelle = "Organisation",
						DirectionId = 1
					},
					new Departement()
					{
						Id = 4,
						Code = "CLI",
						Libelle = "Chargé clientèle",
						DirectionId = 2
					},
					new Departement()
					{
						Id = 5,
						Code = "ANL",
						Libelle = "Analyse",
						DirectionId = 2
					}
				);
			builder
				.Entity<Profil>().HasData(

					new Profil()
					{
						Id = 1,
						Code = "PROFIL-001",
						Libelle = "Commercial",
						Description = "Profil destiné aux commerciaux",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = "03/09/2024"
					},
					new Profil()
					{
						Id = 2,
						Code = "PROFIL-002",
						Libelle = "Informatique",
						Description = "Profil destiné aux informaticiens",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = "03/09/2024"
					},
					new Profil()
					{
						Id = 3,
						Code = "PROFIL-003",
						Libelle = "Analyste",
						Description = "Profil destiné aux analystes",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = "03/09/2024"
					}
				);
			builder.Entity<HabilitationProfil>().HasData(
				new HabilitationProfil()
				{
					Id = 1,
					Edition = true,
					Insertion = true,
					Modification = true,
					Generation = true,
					Suppression = true,
					ProfilId = 2,
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
				},
				new HabilitationProfil()
				{
					Id = 2,
					Edition = true,
					Insertion = true,
					Modification = true,
					Generation = false,
					Suppression = false,
					ProfilId = 1,
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
				},
				new HabilitationProfil()
				{
					Id = 3,
					Edition = true,
					Insertion = true,
					Modification = true,
					Generation = false,
					Suppression = false,
					ProfilId = 3,
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
				}
			);


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