using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    ApplicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Indice = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prenoms = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateNaissance = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LieuNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressePostale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Residence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quartier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoitePostale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatureQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganeDecisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganeDecisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrageFraisDossiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plancher = table.Column<double>(type: "float", nullable: false),
                    Plafond = table.Column<double>(type: "float", nullable: false),
                    PourcentageCommissionEngagement = table.Column<int>(type: "int", nullable: false),
                    FraisFixe = table.Column<double>(type: "float", nullable: false),
                    FraisDossiers = table.Column<double>(type: "float", nullable: false),
                    PourcentageTAF = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrageFraisDossiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamGlobals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncludeDigits = table.Column<bool>(type: "bit", nullable: false),
                    IncludeLowerCase = table.Column<bool>(type: "bit", nullable: false),
                    IncludeUpperCase = table.Column<bool>(type: "bit", nullable: false),
                    IncludeSpecialCharacters = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeUsername = table.Column<bool>(type: "bit", nullable: false),
                    Taille = table.Column<int>(type: "int", nullable: false),
                    DelaiExpiration = table.Column<int>(type: "int", nullable: false),
                    SmtpEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamGlobals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicitePaiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicitePaiements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateExpiration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatutMaritals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutMaritals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeJournaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeJournaux", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypePrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePrets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCompte = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Solde = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comptes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DirectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departements_Directions_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleOrganes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DureeTraitement = table.Column<int>(type: "int", nullable: false),
                    OrganeDecisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOrganes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleOrganes_OrganeDecisions_OrganeDecisionId",
                        column: x => x.OrganeDecisionId,
                        principalTable: "OrganeDecisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HabilitationProfils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edition = table.Column<bool>(type: "bit", nullable: false),
                    Insertion = table.Column<bool>(type: "bit", nullable: false),
                    Modification = table.Column<bool>(type: "bit", nullable: false),
                    Suppression = table.Column<bool>(type: "bit", nullable: false),
                    Generation = table.Column<bool>(type: "bit", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilitationProfils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilitationProfils_Profils_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEditPassword = table.Column<bool>(type: "bit", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    DateExpirationCompte = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateDesactivation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateModificationMotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Profils_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DossierClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDossier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taille = table.Column<double>(type: "float", nullable: false),
                    Poids = table.Column<double>(type: "float", nullable: false),
                    TensionArterielle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fumeur = table.Column<bool>(type: "bit", nullable: false),
                    NbrCigarettes = table.Column<int>(type: "int", nullable: false),
                    BuveurOccasionnel = table.Column<bool>(type: "bit", nullable: false),
                    BuveurRegulier = table.Column<bool>(type: "bit", nullable: false),
                    Distractions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstSportif = table.Column<bool>(type: "bit", nullable: false),
                    CategorieSport = table.Column<int>(type: "int", nullable: false),
                    EstInfirme = table.Column<bool>(type: "bit", nullable: false),
                    NatureInfirmite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSurvenance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatutMaritalId = table.Column<int>(type: "int", nullable: false),
                    Cloturer = table.Column<bool>(type: "bit", nullable: false),
                    DateCloture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSoumission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouvertureEmprunteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarteIdentite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContratTravail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttestationTravail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremierBulletinSalaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeuxiemeBulletinSalaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TroisiemeBulletinSalaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactureProFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EcheanceCarteIdentite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossierClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DossierClients_StatutMaritals_StatutMaritalId",
                        column: x => x.StatutMaritalId,
                        principalTable: "StatutMaritals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Niveau = table.Column<int>(type: "int", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Machine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peripherique = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Navigateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MethodeHTTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferenceURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOperation = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateSysteme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TypeJournalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journaux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journaux_TypeJournaux_TypeJournalId",
                        column: x => x.TypeJournalId,
                        principalTable: "TypeJournaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deroulements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plancher = table.Column<double>(type: "float", nullable: false),
                    Plafond = table.Column<double>(type: "float", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NiveauInstance = table.Column<int>(type: "int", nullable: false),
                    TypePretId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deroulements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deroulements_TypePrets_TypePretId",
                        column: x => x.TypePretId,
                        principalTable: "TypePrets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    HabilitationProfilId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menus_HabilitationProfils_HabilitationProfilId",
                        column: x => x.HabilitationProfilId,
                        principalTable: "HabilitationProfils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricule = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prenoms = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employes_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employes_Utilisateurs_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembreOrganes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganeDecisionId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembreOrganes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembreOrganes_OrganeDecisions_OrganeDecisionId",
                        column: x => x.OrganeDecisionId,
                        principalTable: "OrganeDecisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembreOrganes_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotDePasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldPasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldPasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "03/08/2023"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotDePasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotDePasses_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoSanteClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReponseBoolenne = table.Column<bool>(type: "bit", nullable: false),
                    ReponsePrecision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DureeTraitement = table.Column<int>(type: "int", nullable: false),
                    PeriodeTraitement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LieuTraitement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NatureQuestionId = table.Column<int>(type: "int", nullable: false),
                    DossierClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSanteClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoSanteClients_DossierClients_DossierClientId",
                        column: x => x.DossierClientId,
                        principalTable: "DossierClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfoSanteClients_NatureQuestions_NatureQuestionId",
                        column: x => x.NatureQuestionId,
                        principalTable: "NatureQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PretAccords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontantPret = table.Column<double>(type: "float", nullable: false),
                    DatePremiereEcheance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDerniereEcheance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontantPrime = table.Column<double>(type: "float", nullable: false),
                    Surprime = table.Column<double>(type: "float", nullable: false),
                    PrimeTotale = table.Column<double>(type: "float", nullable: false),
                    SalaireNetMensuel = table.Column<double>(type: "float", nullable: false),
                    QuotiteCessible = table.Column<double>(type: "float", nullable: false),
                    Mensualite = table.Column<double>(type: "float", nullable: false),
                    TauxEngagement = table.Column<int>(type: "int", nullable: false),
                    DateDepartRetraite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypePretId = table.Column<int>(type: "int", nullable: false),
                    PeriodicitePaiementId = table.Column<int>(type: "int", nullable: false),
                    DossierClientId = table.Column<int>(type: "int", nullable: false),
                    EmployeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PretAccords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PretAccords_DossierClients_DossierClientId",
                        column: x => x.DossierClientId,
                        principalTable: "DossierClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PretAccords_Employeurs_EmployeurId",
                        column: x => x.EmployeurId,
                        principalTable: "Employeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PretAccords_PeriodicitePaiements_PeriodicitePaiementId",
                        column: x => x.PeriodicitePaiementId,
                        principalTable: "PeriodicitePaiements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PretAccords_TypePrets_TypePretId",
                        column: x => x.TypePretId,
                        principalTable: "TypePrets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EtapeDeroulements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Etape = table.Column<int>(type: "int", nullable: false),
                    DeroulementId = table.Column<int>(type: "int", nullable: false),
                    MembreOrganeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapeDeroulements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapeDeroulements_Deroulements_DeroulementId",
                        column: x => x.DeroulementId,
                        principalTable: "Deroulements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EtapeDeroulements_MembreOrganes_MembreOrganeId",
                        column: x => x.MembreOrganeId,
                        principalTable: "MembreOrganes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatutDossierClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReception = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecisionFinale = table.Column<bool>(type: "bit", nullable: true),
                    Motif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EtapeDeroulementId = table.Column<int>(type: "int", nullable: false),
                    DossierClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutDossierClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatutDossierClients_DossierClients_DossierClientId",
                        column: x => x.DossierClientId,
                        principalTable: "DossierClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatutDossierClients_EtapeDeroulements_EtapeDeroulementId",
                        column: x => x.EtapeDeroulementId,
                        principalTable: "EtapeDeroulements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Directions",
                columns: new[] { "Id", "Code", "Libelle" },
                values: new object[,]
                {
                    { 1, "DSIOSI", "Direction informatique" },
                    { 2, "DRC", "Direction commerciale" },
                    { 3, "GGE", "Direction gestion des engagements" },
                    { 4, "GAR", "Direction rédaction des garanties" }
                });

            migrationBuilder.InsertData(
                table: "NatureQuestions",
                columns: new[] { "Id", "Libelle" },
                values: new object[,]
                {
                    { 1, "Avez-vous été malade au cours des 6 derniers mois ?" },
                    { 2, "Êtes vous souvent fatigué (e)?" },
                    { 3, "Avez-vous maigri les 6 derniers mois ?" },
                    { 4, "Avez-vous des ganglions, des furoncles, des abcès ou des maladies de la peau ?" },
                    { 5, "Toussez-vous depuis quelque temps avec en plus de la fièvre ?" },
                    { 6, "Avez-vous des plaies dans la bouche ?" },
                    { 7, "Faites-vous souvent la diarrhée ?" },
                    { 8, "Êtes vous souvent ballonné (e) ?" },
                    { 9, "Avez-vous des OEdèmes des Membres Inférieurs (O.M.I) ?" },
                    { 10, "Êtes vous essoufflé (e) au moindre effort ?" },
                    { 11, "Avez-vous déjà reçu une perfusion ?" },
                    { 12, "Avez-vous déjà reçu une transfusion de sang ?" },
                    { 13, "Avez-vous déjà subi une opération ?" },
                    { 14, "Avez-vous des informations complémentaires sur votre état de santé susceptibles de renseignerl'assureur ?" }
                });

            migrationBuilder.InsertData(
                table: "ParametrageFraisDossiers",
                columns: new[] { "Id", "FraisDossiers", "FraisFixe", "Plafond", "Plancher", "PourcentageCommissionEngagement", "PourcentageTAF" },
                values: new object[,]
                {
                    { 1, 0.0, 0.0, 100000.0, 1.0, 0, 0 },
                    { 2, 10000.0, 1000.0, 1000000.0, 500001.0, 25, 10 },
                    { 3, 5000.0, 1000.0, 500000.0, 100001.0, 25, 10 },
                    { 4, 25000.0, 1000.0, 2000000.0, 1000001.0, 25, 10 },
                    { 5, 50000.0, 1000.0, 5000000.0, 2000001.0, 25, 10 },
                    { 6, 65000.0, 1000.0, 0.0, 5000001.0, 25, 10 }
                });

            migrationBuilder.InsertData(
                table: "PeriodicitePaiements",
                columns: new[] { "Id", "Libelle" },
                values: new object[,]
                {
                    { 1, "Unique" },
                    { 2, "Mensuelle" },
                    { 3, "Annuelle" }
                });

            migrationBuilder.InsertData(
                table: "Profils",
                columns: new[] { "Id", "Code", "DateAjout", "DateExpiration", "DateModification", "Description", "Libelle", "Statut" },
                values: new object[,]
                {
                    { 1, "PROFIL-001", "03/08/2023", "03/08/2024", "", "Profil destiné à l'administrateur", "Administrateur", 1 },
                    { 2, "PROFIL-002", "03/08/2023", "03/08/2024", "", "Profil destiné aux au gestionnaire", "Gestionnaire", 1 },
                    { 3, "PROFIL-003", "03/08/2023", "03/08/2024", "", "Profil destiné à l'analyste", "Analyste", 1 },
                    { 4, "PROFIL-004", "03/08/2023", "03/08/2024", "", "Profil destiné au directeur de la GGR", "Directeur GGR", 1 },
                    { 5, "PROFIL-005", "03/08/2023", "03/08/2024", "", "Profil destiné aux utilisateurs", "Chef Departement Back-Office Engagement", 1 }
                });

            migrationBuilder.InsertData(
                table: "StatutMaritals",
                columns: new[] { "Id", "Libelle" },
                values: new object[,]
                {
                    { 1, "Célibataire" },
                    { 2, "Divorcé(e)" },
                    { 3, "Marié(e)" },
                    { 4, "Divorcé(e)" }
                });

            migrationBuilder.InsertData(
                table: "TypeJournaux",
                columns: new[] { "Id", "Code", "Libelle", "Statut" },
                values: new object[,]
                {
                    { 1, "CONN", "Connexion", 0 },
                    { 2, "DECONN", "Déconnexion", 0 },
                    { 3, "UPDATE", "Modification de données", 0 },
                    { 4, "DELETE", "Suppression des données", 0 },
                    { 5, "ADD", "Ajout de données", 0 },
                    { 6, "GETBYID", "Rechercher un objet par son id", 0 },
                    { 7, "GETBYCODE", "Rechercher un objet par son code", 0 },
                    { 8, "GET", "Récupération de données", 0 },
                    { 9, "MNT", "Montage de dossier crédit", 0 },
                    { 10, "GEN", "Génération de fiches d'assurance", 0 }
                });

            migrationBuilder.InsertData(
                table: "TypePrets",
                columns: new[] { "Id", "Libelle" },
                values: new object[,]
                {
                    { 1, "Court terme (1 à 2 ans)" },
                    { 2, "Court terme (2 à 4 ans)" },
                    { 3, "Découvert" },
                    { 4, "Crédit Moyen Terme" },
                    { 5, "C.D.M.H" },
                    { 6, "Autre Prêt Immobilier" }
                });

            migrationBuilder.InsertData(
                table: "Departements",
                columns: new[] { "Id", "Code", "DirectionId", "Libelle" },
                values: new object[,]
                {
                    { 1, "ETUDEV", 1, "Etudes et développement" },
                    { 2, "EXPL", 1, "Exploitation" },
                    { 3, "ORG", 1, "Organisation" },
                    { 4, "CLI", 2, "Chargé clientèle" },
                    { 5, "ANL", 2, "Analyse" }
                });

            migrationBuilder.InsertData(
                table: "HabilitationProfils",
                columns: new[] { "Id", "DateAjout", "DateModification", "Edition", "Generation", "Insertion", "Modification", "ProfilId", "Suppression" },
                values: new object[,]
                {
                    { 1, "03/08/2023", "", true, true, true, true, 1, true },
                    { 2, "03/08/2023", "", true, false, true, true, 2, false },
                    { 3, "03/08/2023", "", true, false, true, true, 3, false },
                    { 4, "03/08/2023", "", true, false, true, true, 4, false },
                    { 5, "03/08/2023", "", true, false, true, true, 5, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Code",
                table: "Applications",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Indice",
                table: "Clients",
                column: "Indice",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_ClientId",
                table: "Comptes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_NumeroCompte",
                table: "Comptes",
                column: "NumeroCompte",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departements_Code",
                table: "Departements",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departements_DirectionId",
                table: "Departements",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Deroulements_TypePretId",
                table: "Deroulements",
                column: "TypePretId");

            migrationBuilder.CreateIndex(
                name: "IX_Directions_Code",
                table: "Directions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DossierClients_ClientId",
                table: "DossierClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DossierClients_StatutMaritalId",
                table: "DossierClients",
                column: "StatutMaritalId");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_DepartementId",
                table: "Employes",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_Email",
                table: "Employes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_Matricule",
                table: "Employes",
                column: "Matricule",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_UserId",
                table: "Employes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employeurs_BoitePostale",
                table: "Employeurs",
                column: "BoitePostale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employeurs_Tel",
                table: "Employeurs",
                column: "Tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EtapeDeroulements_DeroulementId",
                table: "EtapeDeroulements",
                column: "DeroulementId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapeDeroulements_Id",
                table: "EtapeDeroulements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EtapeDeroulements_MembreOrganeId",
                table: "EtapeDeroulements",
                column: "MembreOrganeId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilitationProfils_ProfilId",
                table: "HabilitationProfils",
                column: "ProfilId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoSanteClients_DossierClientId",
                table: "InfoSanteClients",
                column: "DossierClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoSanteClients_NatureQuestionId",
                table: "InfoSanteClients",
                column: "NatureQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Journaux_TypeJournalId",
                table: "Journaux",
                column: "TypeJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_MembreOrganes_OrganeDecisionId",
                table: "MembreOrganes",
                column: "OrganeDecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_MembreOrganes_UtilisateurId",
                table: "MembreOrganes",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ApplicationId",
                table: "Menus",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Code",
                table: "Menus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_HabilitationProfilId",
                table: "Menus",
                column: "HabilitationProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuId",
                table: "Menus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MotDePasses_UtilisateurId",
                table: "MotDePasses",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_PretAccords_DossierClientId",
                table: "PretAccords",
                column: "DossierClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PretAccords_EmployeurId",
                table: "PretAccords",
                column: "EmployeurId");

            migrationBuilder.CreateIndex(
                name: "IX_PretAccords_PeriodicitePaiementId",
                table: "PretAccords",
                column: "PeriodicitePaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_PretAccords_TypePretId",
                table: "PretAccords",
                column: "TypePretId");

            migrationBuilder.CreateIndex(
                name: "IX_Profils_Code",
                table: "Profils",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleOrganes_OrganeDecisionId",
                table: "RoleOrganes",
                column: "OrganeDecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StatutDossierClients_DossierClientId",
                table: "StatutDossierClients",
                column: "DossierClientId");

            migrationBuilder.CreateIndex(
                name: "IX_StatutDossierClients_EtapeDeroulementId",
                table: "StatutDossierClients",
                column: "EtapeDeroulementId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeJournaux_Code",
                table: "TypeJournaux",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_ProfilId",
                table: "Utilisateurs",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Username",
                table: "Utilisateurs",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Employes");

            migrationBuilder.DropTable(
                name: "InfoSanteClients");

            migrationBuilder.DropTable(
                name: "Journaux");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "MotDePasses");

            migrationBuilder.DropTable(
                name: "ParametrageFraisDossiers");

            migrationBuilder.DropTable(
                name: "ParamGlobals");

            migrationBuilder.DropTable(
                name: "PretAccords");

            migrationBuilder.DropTable(
                name: "RoleOrganes");

            migrationBuilder.DropTable(
                name: "StatutDossierClients");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropTable(
                name: "NatureQuestions");

            migrationBuilder.DropTable(
                name: "TypeJournaux");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "HabilitationProfils");

            migrationBuilder.DropTable(
                name: "Employeurs");

            migrationBuilder.DropTable(
                name: "PeriodicitePaiements");

            migrationBuilder.DropTable(
                name: "DossierClients");

            migrationBuilder.DropTable(
                name: "EtapeDeroulements");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "StatutMaritals");

            migrationBuilder.DropTable(
                name: "Deroulements");

            migrationBuilder.DropTable(
                name: "MembreOrganes");

            migrationBuilder.DropTable(
                name: "TypePrets");

            migrationBuilder.DropTable(
                name: "OrganeDecisions");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Profils");
        }
    }
}
