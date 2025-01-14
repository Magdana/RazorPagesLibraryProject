using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesLibraryProject.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cb561d3-484d-45ed-9440-79b08769e324", "915cae38-10d4-4082-9e8e-9aab14ef81a1", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75e18f84-1b22-44ca-9b79-f6e3e81b87b8", "4a293db1-d6e5-40ff-aaee-00c4087bdd4c", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cb561d3-484d-45ed-9440-79b08769e324");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75e18f84-1b22-44ca-9b79-f6e3e81b87b8");
        }
    }
}
