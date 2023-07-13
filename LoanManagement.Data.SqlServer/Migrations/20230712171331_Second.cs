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
            migrationBuilder.DropForeignKey(
                name: "FK_Employes_MembreOrganes_OrganeId",
                table: "Employes");

            migrationBuilder.DropIndex(
                name: "IX_Employes_OrganeId",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "OrganeId",
                table: "Employes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganeId",
                table: "Employes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_OrganeId",
                table: "Employes",
                column: "OrganeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_MembreOrganes_OrganeId",
                table: "Employes",
                column: "OrganeId",
                principalTable: "MembreOrganes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
