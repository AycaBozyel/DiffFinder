using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiffFinder.Migrations
{
    public partial class _202205140320 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "DiffrenceInformation",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "DiffsOffsets",
                columns: table => new
                {
                    DiffsOffsetsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiffrenceInformationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Diffs = table.Column<int>(type: "INTEGER", nullable: false),
                    Offset = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiffsOffsets", x => x.DiffsOffsetsId);
                    table.ForeignKey(
                        name: "FK_DiffsOffsets_DiffrenceInformation_DiffrenceInformationId",
                        column: x => x.DiffrenceInformationId,
                        principalTable: "DiffrenceInformation",
                        principalColumn: "DifferenceInformationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiffsOffsets_DiffrenceInformationId",
                table: "DiffsOffsets",
                column: "DiffrenceInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiffsOffsets");

            migrationBuilder.AlterColumn<bool>(
                name: "Result",
                table: "DiffrenceInformation",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
