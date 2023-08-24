using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class loan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AddColumn<double>(
                name: "Montant",
                table: "DossierClients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "24/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "Employes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateModification" },
                values: new object[] { "24/08/2023", "24/08/2023" });

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAjout",
                value: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAjout",
                value: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "24/08/2023", "24/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "24/08/2023", "24/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "24/08/2023", "24/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "24/08/2023", "24/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "24/08/2023", "24/08/2024" });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateModificationMotDePasse", "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "24/08/2023", "24/08/2023", "B70D378172857406B7B0EAB74592CBDE453C31B333C67F95D940575376F57E6B6C92109A66D02D3B4BCC758C8CE7C4BBFB05999E28C13F6FFF8EEA17102A6776", new byte[] { 167, 23, 45, 38, 53, 198, 31, 196, 30, 250, 196, 193, 236, 21, 0, 241, 54, 62, 236, 122, 49, 230, 147, 204, 90, 20, 166, 33, 232, 148, 250, 234, 135, 81, 41, 193, 70, 210, 129, 182, 187, 182, 191, 192, 193, 70, 153, 228, 30, 241, 164, 153, 25, 240, 148, 79, 215, 1, 96, 61, 126, 125, 17, 117 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwibmJmIjoxNjkyODM0NTI4LCJleHAiOjE2OTM0MzkzMjh9.owp1u48lFNZs1hyhDUFtIT8VTP6EUt8bqScKKd75520" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Montant",
                table: "DossierClients");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "24/08/2023");

            migrationBuilder.UpdateData(
                table: "Employes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateModification" },
                values: new object[] { "23/08/2023", "23/08/2023" });

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAjout",
                value: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAjout",
                value: "23/08/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "23/08/2023", "23/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "23/08/2023", "23/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "23/08/2023", "23/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "23/08/2023", "23/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "23/08/2023", "23/08/2024" });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateModificationMotDePasse", "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "23/08/2023", "23/08/2023", "89D5327C40C8DB213126BDAC94FF7BC44FAF0EDB9247A8B594E1C76D44DD15F6E0D80747F04A812C80DCD49B78D3843963E6012CE9E34D43242AD64D3A0750C3", new byte[] { 57, 116, 129, 228, 245, 200, 207, 110, 45, 17, 252, 84, 112, 64, 138, 75, 120, 225, 88, 30, 151, 67, 91, 17, 52, 43, 32, 83, 238, 50, 131, 255, 10, 245, 11, 58, 128, 72, 101, 170, 190, 168, 137, 140, 48, 152, 52, 56, 129, 228, 35, 111, 19, 83, 248, 34, 136, 47, 224, 249, 58, 253, 36, 90 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwibmJmIjoxNjkyODIxMDc4LCJleHAiOjE2OTM0MjU4Nzh9._ng0wE6DBCoXjtUafWQwG04TyCjjhN0fQmCLDye1WIc" });
        }
    }
}
