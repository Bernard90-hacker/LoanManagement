using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
                    ApplicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
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
                name: "ParamMotDePasses",
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
                    DelaiExpiration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamMotDePasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateExpiration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profils", x => x.Id);
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
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
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
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
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
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prenoms = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
                    DateModification = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
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
                    DateOperation = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
                    DateSysteme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Journaux_Utilisateurs_UtilisateurId",
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
                    DateAjout = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "29/06/2023"),
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

            migrationBuilder.InsertData(
                table: "ParamMotDePasses",
                columns: new[] { "Id", "DelaiExpiration", "ExcludeUsername", "IncludeDigits", "IncludeLowerCase", "IncludeSpecialCharacters", "IncludeUpperCase", "Taille" },
                values: new object[] { 1, 6, true, true, true, true, true, 8 });

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
                    { 7, "GETBYCODE", "Rechercher un objet par son code", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Code",
                table: "Applications",
                column: "Code",
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
                name: "IX_Directions_Code",
                table: "Directions",
                column: "Code",
                unique: true);

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
                name: "IX_Employes_UserId",
                table: "Employes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HabilitationProfils_ProfilId",
                table: "HabilitationProfils",
                column: "ProfilId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journaux_TypeJournalId",
                table: "Journaux",
                column: "TypeJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Journaux_UtilisateurId",
                table: "Journaux",
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
                name: "IX_Profils_Code",
                table: "Profils",
                column: "Code",
                unique: true);

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
                name: "Employes");

            migrationBuilder.DropTable(
                name: "Journaux");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "MotDePasses");

            migrationBuilder.DropTable(
                name: "ParamMotDePasses");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropTable(
                name: "TypeJournaux");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "HabilitationProfils");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "Profils");
        }
    }
}
