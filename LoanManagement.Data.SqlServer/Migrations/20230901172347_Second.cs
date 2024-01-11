using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EtapeDeroulements",
                columns: new[] { "Id", "DeroulementId", "Etape", "MembreOrganeId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 1, 1 },
                    { 4, 4, 1, 1 },
                    { 5, 5, 1, 1 },
                    { 6, 6, 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "8DBCA53FC4B96AB9B25D6604686AD054716084AE32DE86C79FC4355BCEC4478B5DFD967DF31D577E80F3A4FAF723DE0A5E31EA55EF896B388947361F8C4F7766", new byte[] { 238, 58, 173, 213, 100, 37, 50, 86, 9, 91, 93, 38, 141, 85, 100, 205, 66, 158, 231, 92, 188, 16, 73, 76, 94, 162, 237, 186, 18, 50, 28, 106, 169, 163, 209, 252, 2, 76, 118, 131, 210, 156, 154, 173, 87, 198, 114, 131, 33, 233, 96, 100, 227, 183, 120, 175, 65, 36, 49, 53, 63, 66, 147, 228 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwibmJmIjoxNjkzNTg5MDI3LCJleHAiOjE2OTQxOTM4Mjd9.3ZVbCxM3CTeXBNJOLyB-E2eQ5Cknq1KkkLO60Dl5EXU" });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "6FF2ACE95A084659D3AE7522C1CEE5BB182FAA3362BE1F7CF02106CD607EE635FE307B797BD5202DE1D2D02FE36E02B7883267C1D3AB0A6BACBCC5DBF4AC99C7", new byte[] { 21, 123, 157, 24, 207, 49, 234, 37, 55, 231, 2, 199, 202, 168, 126, 88, 253, 226, 75, 79, 104, 129, 226, 149, 109, 119, 26, 45, 170, 211, 167, 218, 84, 118, 17, 156, 82, 27, 253, 78, 162, 43, 74, 135, 170, 90, 216, 161, 123, 97, 102, 249, 87, 143, 248, 148, 38, 121, 37, 75, 31, 72, 28, 83 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Indhem91IiwibmJmIjoxNjkzNTg5MDI3LCJleHAiOjE2OTQxOTM4Mjd9.eY6dgOSskTPlL81sa2xZuw4g5DkK7Pz_jE_UKLiby-4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EtapeDeroulements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "43B6FE4F85C1E392D1F7341EC43164B37FC57DF19B936200AD735A0255D619A644EA6280E5FAC80C6190ABBB1C0832DC1239BE7911FE724BCB90B16ADBBA55CF", new byte[] { 2, 38, 34, 52, 8, 114, 251, 11, 139, 162, 152, 133, 87, 122, 80, 18, 161, 1, 249, 5, 6, 128, 56, 183, 35, 43, 51, 228, 103, 206, 80, 120, 135, 163, 108, 97, 107, 214, 124, 76, 95, 98, 6, 192, 155, 38, 178, 138, 136, 129, 131, 214, 237, 89, 162, 124, 73, 45, 161, 133, 179, 153, 11, 69 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwibmJmIjoxNjkzNTg4NzA4LCJleHAiOjE2OTQxOTM1MDh9.eUQhoKks4oDiMIZG6KnZL1Dmmm-ox3skotPpT4Sucq4" });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "9D9B4DF8B59F183A72E0D3896CDB15548390C15CE4FCF93F73463CA14E28BED81A2C4BD94D67E24208659D4D56EEB6C5173FC95E0A2073B9511CF5E93480F6C0", new byte[] { 89, 11, 30, 108, 162, 42, 120, 131, 214, 101, 129, 212, 247, 127, 62, 125, 186, 119, 75, 213, 254, 167, 25, 28, 31, 150, 201, 95, 106, 129, 180, 149, 76, 235, 146, 64, 248, 47, 188, 78, 105, 213, 2, 79, 157, 187, 194, 92, 150, 35, 128, 182, 2, 1, 46, 91, 196, 117, 203, 188, 147, 1, 86, 173 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Indhem91IiwibmJmIjoxNjkzNTg4NzA4LCJleHAiOjE2OTQxOTM1MDh9.5mYPc5drhBmphqzWHedopE1ZuIt2ZJmeRDf4Qph9UmE" });
        }
    }
}
