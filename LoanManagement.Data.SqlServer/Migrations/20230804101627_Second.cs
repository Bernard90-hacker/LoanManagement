using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuveurOccasionnel",
                table: "DossierClients");

            migrationBuilder.DropColumn(
                name: "BuveurRegulier",
                table: "DossierClients");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AddColumn<int>(
                name: "Buveur",
                table: "DossierClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "04/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAjout",
                value: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAjout",
                value: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "04/08/2023", "04/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "04/08/2023", "04/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "04/08/2023", "04/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "04/08/2023", "04/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "04/08/2023", "04/08/2024" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buveur",
                table: "DossierClients");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Profils",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "MotDePasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateOperation",
                table: "Journaux",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AddColumn<bool>(
                name: "BuveurOccasionnel",
                table: "DossierClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BuveurRegulier",
                table: "DossierClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DateModification",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.AlterColumn<string>(
                name: "DateAjout",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "03/08/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "04/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAjout",
                value: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAjout",
                value: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAjout",
                value: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAjout",
                value: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "HabilitationProfils",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAjout",
                value: "03/08/2023");

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "03/08/2023", "03/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "03/08/2023", "03/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "03/08/2023", "03/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "03/08/2023", "03/08/2024" });

            migrationBuilder.UpdateData(
                table: "Profils",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAjout", "DateExpiration" },
                values: new object[] { "03/08/2023", "03/08/2024" });
        }
    }
}
