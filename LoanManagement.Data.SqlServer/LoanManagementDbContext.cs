using LoanManagement.core.Models.Loan_Management;
using LoanManagement.Data.SqlServer.Configuration.Loan_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace LoanManagement.Data.SqlServer
{
    public class LoanManagementDbContext : DbContext
    {
		private readonly IConfiguration _configuration;
        public LoanManagementDbContext(DbContextOptions<LoanManagementDbContext> options, IConfiguration configuration) : base(options) 
		{
			_configuration = configuration;
		}
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
				.ApplyConfiguration(new ParamGlobalConfiguration());

			builder
				.ApplyConfiguration(new ApplicationConfiguration());

			builder
				.ApplyConfiguration(new ProfilConfiguration());

			builder
				.ApplyConfiguration(new TypeJournalConfiguration());

			builder
				.ApplyConfiguration(new UtilisateurConfiguration());

			builder
				.ApplyConfiguration(new ClientConfiguration());

			builder
				.ApplyConfiguration(new CompteConfiguration());

			builder
				.ApplyConfiguration(new DeroulementConfiguration());

			builder
				.ApplyConfiguration(new DossierClientConfiguration());

			builder
				.ApplyConfiguration(new EmployeurConfiguration());

			builder
				.ApplyConfiguration(new EtapeDeroulementConfiguration());

			builder
				.ApplyConfiguration(new InfoSanteClientConfiguration());

			builder
				.ApplyConfiguration(new MembreOrganeConfiguration());

			builder
				.ApplyConfiguration(new NatureQuestionConfiguration());

			builder
				.ApplyConfiguration(new OrganeDecisionConfiguration());

			builder
				.ApplyConfiguration(new PeriodicitePaiementConfiguration());

			builder
				.ApplyConfiguration(new PeriodicitePaiementConfiguration());

			builder
				.ApplyConfiguration(new PretAccordConfiguration());

			builder
				.ApplyConfiguration(new RoleOrganeConfiguration());

			builder
				.ApplyConfiguration(new StatutDossierClientConfiguration());

			builder
				.ApplyConfiguration(new StatutMaritalConfiguration());

			builder
				.ApplyConfiguration(new TypePretConfiguration());

			builder
				.ApplyConfiguration(new ParametrageFraisDossierConfiguration());



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
					},
					new TypeJournal()
					{
						Id = 10,
						Code = "GEN",
						Libelle = "Génération de fiches d'assurance"
					}
				);

			builder
				.Entity<ParamGlobal>().HasData(

					new ParamGlobal()
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
					},
					new Direction()
					{
						Id = 4,
						Code = "GGR",
						Libelle = "Direction gestion globale des risques."
					},
					new Direction()
					{
						Id = 5,
						Code = "GAR",
						Libelle = "Direction rédaction des garanties."
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
			var x = DateTime.Now.Year + 1;
			var y = DateTime.Now.ToString("dd/MM/");
			builder
				.Entity<Profil>().HasData(

					new Profil()
					{
						Id = 1,
						Code = "PROFIL-001",
						Libelle = "Administrateur",
						Description = "Profil destiné à l'administrateur",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = y + x.ToString()
					},
					new Profil()
					{
						Id = 2,
						Code = "PROFIL-002",
						Libelle = "Gestionnaire",
						Description = "Profil destiné aux au gestionnaire",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = y + x.ToString()
					},
					new Profil()
					{
						Id = 3,
						Code = "PROFIL-003",
						Libelle = "Analyste",
						Description = "Profil destiné à l'analyste",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = y + x.ToString()
					},
					new Profil()
					{
						Id = 4,
						Code = "PROFIL-004",
						Libelle = "Directeur GGR",
						Description = "Profil destiné au directeur de la GGR",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						Statut = 1,
						DateExpiration = y + x.ToString()
					},
                    new Profil()
                    {
                        Id = 5,
                        Code = "PROFIL-005",
                        Libelle = "Chef Departement Back-Office Engagement",
                        Description = "Profil destiné aux utilisateurs",
                        DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
                        Statut = 1,
                        DateExpiration = y + x.ToString()
                    }
                ) ; 
			builder.Entity<HabilitationProfil>().HasData(
				new HabilitationProfil()
				{
					Id = 1,
					Edition = true,
					Insertion = true,
					Modification = true,
					Generation = true,
					Suppression = true,
					ProfilId = 1,
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
					ProfilId = 2,
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
                },
                new HabilitationProfil()
                {
                    Id = 4,
                    Edition = true,
                    Insertion = true,
                    Modification = true,
                    Generation = false,
                    Suppression = false,
                    ProfilId = 4,
                    DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
                }
				,
                new HabilitationProfil()
                {
                    Id = 5,
                    Edition = true,
                    Insertion = true,
                    Modification = true,
                    Generation = false,
                    Suppression = false,
                    ProfilId = 5,
                    DateAjout = DateTime.Now.ToString("dd/MM/yyyy")
                }
            );
            UtilsConstant.HashPassword("skyrocker96S@", out byte[] passwordSalt, out string passwordHashed);
			var token = UtilsConstant.CreateRefreshToken("Admin", _configuration.GetSection("JWT:Key").Value, out string refreshTokenTime);
            builder.Entity<Utilisateur>().HasData(
                new
                {
                    Id = 1,
                    Username = "Admin",
                    PasswordHash = passwordHashed,
                    PasswordSalt = passwordSalt,
                    IsEditPassword = true,
                    IsSuperAdmin = true,
                    IsAdmin = true,
                    Statut = 1,
                    ProfilId = 1,
                    DateExpirationCompte = "20/08/2025",
					DateDesactivation = "20/08/2025",
					DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
					DateModificationMotDePasse = DateTime.Now.ToString("dd/MM/yyyy"),
					IsConnected = false,
                    RefreshToken = token,
                    RefreshTokenTime = refreshTokenTime

                });

            builder.Entity<Client>().HasData
			(
                new
                {
                    Id = 1,
                    Indice = 310,
					Nom = "ATTIOGBE",
					Prenoms = "Godwin",
					Email = "attiogbegodwin2002@gmail.com",
					DateNaissance = "07/10/2002",
					LieuNaissance = "Lomé",
					AdressePostale = "99",
					Residence = "WOGLO",
					Ville = "Lomé",
					Quartier = "ABLOGAME",
					Tel = "70556049",
					Profession = "Etudiant"
                }
			);

            builder.Entity<Employe>().HasData
				(
					new
					{
						Id = 1,
						Matricule = "6060",
						Nom = "KEOULA",
						Prenoms = "Lonlon Edem",
						Email = "ekeoula@gmail.com",
						DateAjout = DateTime.Now.ToString("dd/MM/yyyy"),
						DateModification = DateTime.Now.ToString("dd/MM/yyyy"),
						UserId = 1,
						DepartementId = 1

                    }
				);

            builder
				.Entity<NatureQuestion>()
				.HasData
				(
					new NatureQuestion()
					{
						Id = 1,
						Libelle = "Avez-vous été malade au cours des 6 derniers mois ?"
					},
					new NatureQuestion()
					{
						Id = 2,
						Libelle = "Êtes vous souvent fatigué (e)?"
					},
					new NatureQuestion()
					{
						Id = 3,
						Libelle = "Avez-vous maigri les 6 derniers mois ?"
					},
					new NatureQuestion()
					{
						Id = 4,
						Libelle = "Avez-vous des ganglions, des furoncles, des abcès ou des maladies de la " +
						"peau ?"
					},
					new NatureQuestion()
					{
						Id = 5,
						Libelle = "Toussez-vous depuis quelque temps avec en plus de la fièvre ?"
					},
					new NatureQuestion() {
						Id = 6,
						Libelle = "Avez-vous des plaies dans la bouche ?"
					},
					new NatureQuestion()
					{
						Id = 7,
						Libelle = "Faites-vous souvent la diarrhée ?"
					},
					new NatureQuestion()
					{
						Id = 8,
						Libelle = "Êtes vous souvent ballonné (e) ?"
					},
					new NatureQuestion()
					{
						Id = 9,
						Libelle = "Avez-vous des OEdèmes des Membres Inférieurs (O.M.I) ?"
					},
					new NatureQuestion()
					{
						Id = 10,
						Libelle = "Êtes vous essoufflé (e) au moindre effort ?"
					},
					new NatureQuestion()
					{
						Id = 11,
						Libelle = "Avez-vous déjà reçu une perfusion ?"
					},
					new NatureQuestion()
					{
						Id = 12,
						Libelle = "Avez-vous déjà reçu une transfusion de sang ?"
					},
					new NatureQuestion()
					{
						Id = 13,
						Libelle = "Avez-vous déjà subi une opération ?"
					},
					new NatureQuestion()
					{
						Id = 14,
						Libelle = "Avez-vous des informations complémentaires sur votre état de santé susceptibles de renseigner" +
						"l'assureur ?"
					}
				);

			builder
				.Entity<TypePret>()
				.HasData
				(
					new TypePret()
					{
						Id = 1,
						Libelle = "Court terme (1 à 2 ans)"
					},
					new TypePret()
					{
						Id = 2,
						Libelle = "Court terme (2 à 4 ans)"
					},
					new TypePret()
					{
						Id = 3,
						Libelle = "Découvert"
					},
					new TypePret()
					{
						Id = 4,
						Libelle = "Crédit Moyen Terme"
					},
					new TypePret()
					{
						Id = 5,
						Libelle = "C.D.M.H"
					},
					new TypePret()
					{
						Id = 6,
						Libelle = "Autre Prêt Immobilier"
					}
				);

			builder
				.Entity<ParametrageFraisDossier>()
				.HasData
				(
					new ParametrageFraisDossier()
					{
						Id = 1,
						Plancher = 1,
						Plafond = 100000,
						FraisDossiers = 0,
						PourcentageCommissionEngagement = 0,
						FraisFixe = 0,
						PourcentageTAF = 0
					},
					new ParametrageFraisDossier()
					{
						Id = 2,
						Plancher = 500001,
						Plafond = 1000000,
						FraisDossiers = 10000,
						PourcentageCommissionEngagement = 25,
						FraisFixe = 1000,
						PourcentageTAF = 10
					},
					new ParametrageFraisDossier()
					{
						Id = 3,
						Plancher = 100001,
						Plafond = 500000,
						FraisDossiers = 5000,
						PourcentageCommissionEngagement = 25,
						FraisFixe = 1000,
						PourcentageTAF = 10
					},
					new ParametrageFraisDossier()
					{
						Id = 4,
						Plancher = 1000001,
						Plafond = 2000000,
						FraisDossiers = 25000,
						PourcentageCommissionEngagement = 25,
						FraisFixe = 1000,
						PourcentageTAF = 10
					},
					new ParametrageFraisDossier()
					{
						Id = 5,
						Plancher = 2000001,
						Plafond = 5000000,
						FraisDossiers = 50000,
						PourcentageCommissionEngagement = 25,
						FraisFixe = 1000,
						PourcentageTAF = 10
					},
					new ParametrageFraisDossier()
					{
						Id = 6,
						Plancher = 5000001,
						FraisDossiers = 65000,
						FraisFixe = 1000, 
						PourcentageTAF = 10,
						PourcentageCommissionEngagement = 25
					}

				);

			builder
				.Entity<StatutMarital>()
				.HasData
				(
					new StatutMarital()
					{
						Id = 1,
						Libelle = "Célibataire"
					},
					new StatutMarital()
					{
						Id = 2,
						Libelle = "Divorcé(e)"
					},
					new StatutMarital()
					{
						Id = 3,
						Libelle = "Marié(e)"
					},
					new StatutMarital()
					{
						Id = 4,
						Libelle = "Divorcé(e)"
					}
				);

            builder
                .Entity<PeriodicitePaiement>()
                .HasData
                (
                    new PeriodicitePaiement()
                    {
                        Id = 1,
                        Libelle = "Unique"
                    },
                    new PeriodicitePaiement()
                    {
                        Id = 2,
                        Libelle = "Mensuelle"
                    },
                    new PeriodicitePaiement()
                    {
                        Id = 3,
                        Libelle = "Annuelle"
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
        public DbSet<ParamGlobal> ParamGlobals { get; set; }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<TypeJournal> TypeJournaux { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Deroulement> Deroulements { get; set; }
        public DbSet<DossierClient> DossierClients { get; set; }
        public DbSet<Employeur> Employeurs { get; set; }
        public DbSet<EtapeDeroulement> EtapeDeroulements { get; set; }
        public DbSet<InfoSanteClient> InfoSanteClients { get; set; }
        public DbSet<MembreOrgane> MembreOrganes { get; set; }
        public DbSet<NatureQuestion> NatureQuestions { get; set; }
        public DbSet<OrganeDecision> OrganeDecisions { get; set; }
        public DbSet<PeriodicitePaiement> PeriodicitePaiements { get; set; }
        public DbSet<PretAccord> PretAccords { get; set; }
        public DbSet<RoleOrgane> RoleOrganes { get; set; }
        public DbSet<StatutDossierClient> StatutDossierClients { get; set; }
        public DbSet<StatutMarital> StatutMaritals { get; set; }
        public DbSet<TypePret> TypePrets { get; set; }

    }
}