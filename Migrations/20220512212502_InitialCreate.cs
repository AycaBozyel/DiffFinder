using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiffFinder.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiffrenceInformation",
                columns: table => new
                {
                    DifferenceInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LeftString = table.Column<string>(type: "TEXT", nullable: false),
                    RightString = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiffrenceInformation", x => x.DifferenceInformationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiffrenceInformation");
        }
    }
}
