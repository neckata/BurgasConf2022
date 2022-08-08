using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Shared.Infrastructure.Persistence.Migrations
{
    public partial class AddedActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "OnlineShop",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "OnlineShop",
                table: "Modules");
        }
    }
}
