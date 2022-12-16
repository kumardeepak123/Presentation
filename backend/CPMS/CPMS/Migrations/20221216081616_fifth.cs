using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CPMS.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(15)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Password = table.Column<string>(type: "varchar(8)", maxLength: 10, nullable: true),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 10, nullable: true),
                    Organization = table.Column<string>(type: "varchar(10)", maxLength: 30, nullable: true),
                    ProfileImageName = table.Column<string>(type: "varchar(50)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
