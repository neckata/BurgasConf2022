using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Shared.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                schema: "OnlineShop",
                table: "Modules",
                newName: "IsInSolution");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInSolution",
                schema: "OnlineShop",
                table: "Modules",
                newName: "IsUsed");
        }
    }
}
