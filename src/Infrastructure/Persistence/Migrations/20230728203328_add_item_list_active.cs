using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_App.Infrastructure.Persistence.Migrations
{
    public partial class add_item_list_active : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TodoLists",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "TodoLists");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TodoItems");
        }
    }
}
