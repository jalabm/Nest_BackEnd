using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest_6._03.Migrations
{
    public partial class empty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "navBars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navBars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subNavBars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NavbarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subNavBars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subNavBars_navBars_NavbarId",
                        column: x => x.NavbarId,
                        principalTable: "navBars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subNavBars_NavbarId",
                table: "subNavBars",
                column: "NavbarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subNavBars");

            migrationBuilder.DropTable(
                name: "navBars");
        }
    }
}
