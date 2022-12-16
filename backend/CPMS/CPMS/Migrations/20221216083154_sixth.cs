using Microsoft.EntityFrameworkCore.Migrations;

namespace CPMS.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Clients",
                type: "varchar(8)",
                maxLength: 10,
                nullable: true);
        }
    }
}
