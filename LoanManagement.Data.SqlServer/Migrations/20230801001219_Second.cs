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
            migrationBuilder.AddColumn<double>(
                name: "Port",
                table: "ParamGlobals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "ParamGlobals");
        }
    }
}
