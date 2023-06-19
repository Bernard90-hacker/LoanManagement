using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class DirectionCodeChecking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 19, 38, 22, 904, DateTimeKind.Local).AddTicks(8486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 18, 46, 12, 181, DateTimeKind.Local).AddTicks(2245));

            migrationBuilder.AlterColumn<string>(
                name: "Libelle",
                table: "Directions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 18, 46, 12, 181, DateTimeKind.Local).AddTicks(2245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 19, 38, 22, 904, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.AlterColumn<string>(
                name: "Libelle",
                table: "Directions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
