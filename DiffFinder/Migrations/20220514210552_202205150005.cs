using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiffFinder.Migrations
{
    public partial class _202205150005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "leftChar",
                table: "DiffsOffsets",
                type: "TEXT",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "rightChar",
                table: "DiffsOffsets",
                type: "TEXT",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leftChar",
                table: "DiffsOffsets");

            migrationBuilder.DropColumn(
                name: "rightChar",
                table: "DiffsOffsets");
        }
    }
}
