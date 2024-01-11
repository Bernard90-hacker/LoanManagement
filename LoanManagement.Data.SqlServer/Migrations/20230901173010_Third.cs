using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagement.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DossierTraite",
                table: "DossierClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "044F75BC8AD73F97954BB2B3CD6E85610C0034243B5281D6B7765F319D48DA5ABDD0BDC77D2117D05686E5D3CA97A8628FB7CA231C627FADBA99A96A71C7D171", new byte[] { 191, 143, 111, 229, 73, 68, 79, 136, 142, 186, 167, 149, 43, 188, 134, 26, 70, 221, 189, 199, 218, 150, 157, 183, 117, 29, 155, 171, 49, 14, 187, 113, 91, 96, 239, 249, 69, 140, 7, 144, 38, 66, 100, 221, 125, 35, 43, 239, 134, 17, 103, 158, 223, 164, 192, 219, 124, 0, 217, 236, 110, 131, 112, 165 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwibmJmIjoxNjkzNTg5NDA5LCJleHAiOjE2OTQxOTQyMDl9.imVs9OZX6d7K4rEVzKZDu7MiSMcdWvCTFZJLnbdbmP4" });

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "D9CE18C163F8C74545C27811B4B25A406F21195B4630C041B00BA52AC64F4736EE9661C2BC276A687F9ACB09B89D79E0C9B525049F7D0F3445C1C8B41C8252DD", new byte[] { 21, 68, 214, 56, 118, 210, 86, 122, 110, 215, 173, 70, 27, 57, 161, 26, 240, 8, 40, 137, 212, 130, 207, 115, 183, 137, 25, 176, 156, 181, 110, 255, 12, 141, 53, 61, 59, 121, 211, 173, 4, 210, 26, 141, 145, 40, 109, 16, 217, 3, 119, 207, 133, 248, 215, 163, 72, 99, 199, 143, 19, 222, 17, 125 }, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Indhem91IiwibmJmIjoxNjkzNTg5NDA5LCJleHAiOjE2OTQxOTQyMDl9.XFEtUfKLBLhFUPLwJwxB2874vC2AELNZPFA3psjoDvo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DossierTraite",
                table: "DossierClients");

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
    }
}
