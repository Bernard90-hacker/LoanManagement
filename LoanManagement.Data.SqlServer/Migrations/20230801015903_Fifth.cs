using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PeriodicitePaiements",
                columns: new[] { "Id", "Libelle" },
                values: new object[,]
                {
                    { 1, "Unique" },
                    { 2, "Mensuelle" },
                    { 3, "Annuelle" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PeriodicitePaiements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PeriodicitePaiements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PeriodicitePaiements",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
