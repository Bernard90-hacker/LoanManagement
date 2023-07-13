using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duree",
                table: "TypePrets");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "13/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "13/07/2023");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TypePrets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AddColumn<int>(
                name: "Duree",
                table: "TypePrets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "12/07/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "13/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "12/07/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "12/07/2023");
        }
    }
}
