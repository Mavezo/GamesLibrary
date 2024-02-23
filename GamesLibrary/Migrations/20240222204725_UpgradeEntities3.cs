using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesLibrary.Migrations
{
    public partial class UpgradeEntities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUser");

            migrationBuilder.CreateTable(
                name: "UserGamesRecomendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecomendedGameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGamesRecomendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGamesRecomendation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGamesRecomendation_Games_RecomendedGameId",
                        column: x => x.RecomendedGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGamesRecomendation_RecomendedGameId",
                table: "UserGamesRecomendation",
                column: "RecomendedGameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGamesRecomendation_UserId",
                table: "UserGamesRecomendation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGamesRecomendation");

            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    RecomendationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecomendationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser", x => new { x.RecomendationId, x.RecomendationsId });
                    table.ForeignKey(
                        name: "FK_GameUser_AspNetUsers_RecomendationId",
                        column: x => x.RecomendationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUser_Games_RecomendationsId",
                        column: x => x.RecomendationsId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_RecomendationsId",
                table: "GameUser",
                column: "RecomendationsId");
        }
    }
}
