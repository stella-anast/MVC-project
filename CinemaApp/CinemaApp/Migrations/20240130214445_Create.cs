using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CINEMAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    SEATS = table.Column<int>(type: "int", nullable: false),
                    _3D = table.Column<string>(name: "3D", type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CINEMAS__3214EC27084EA0B1", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "PROVOLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MOVIES_TIME = table.Column<DateTime>(type: "datetime", nullable: false),
                    CINEMAS_ID = table.Column<int>(type: "int", nullable: false),
                    MOVIES_ID = table.Column<int>(type: "int", nullable: false),
                    CONTENT_ADMIN_ID = table.Column<int>(type: "int", nullable: false),
                    SEATS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROVOLES__3214EC27968945E6", x => x.ID);
                    table.ForeignKey(
                        name: "fk_PROVOLES_CINEMAS1",
                        column: x => x.CINEMAS_ID,
                        principalTable: "CINEMAS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_PROVOLES_CONTENT_ADMIN1",
                        column: x => x.CONTENT_ADMIN_ID,
                        principalTable: "CONTENT_ADMIN",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_PROVOLES_MOVIES1",
                        column: x => x.MOVIES_ID,
                        principalTable: "MOVIES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RESERVATIONS",
                columns: table => new
                {
                    CUSTOMERS_ID = table.Column<int>(type: "int", nullable: false),
                    PROVOLES_MOVIES_ID = table.Column<int>(type: "int", nullable: false),
                    NUMBER_OF_SEATS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RESERVAT__8F0BFE192D3A3743", x => new { x.CUSTOMERS_ID, x.PROVOLES_MOVIES_ID });
                    table.ForeignKey(
                        name: "fk_RESERVATIONS_CUSTOMERS1",
                        column: x => x.CUSTOMERS_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_RESERVATIONS_PROVOLES1",
                        column: x => x.PROVOLES_MOVIES_ID,
                        principalTable: "PROVOLES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADMINS_user_username",
                table: "ADMINS",
                column: "user_username");

            migrationBuilder.CreateIndex(
                name: "IX_CONTENT_ADMIN_user_username",
                table: "CONTENT_ADMIN",
                column: "user_username");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_user_username",
                table: "CUSTOMERS",
                column: "user_username");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_CONTENT_ADMIN_ID",
                table: "MOVIES",
                column: "CONTENT_ADMIN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROVOLES_CINEMAS_ID",
                table: "PROVOLES",
                column: "CINEMAS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROVOLES_CONTENT_ADMIN_ID",
                table: "PROVOLES",
                column: "CONTENT_ADMIN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROVOLES_MOVIES_ID",
                table: "PROVOLES",
                column: "MOVIES_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_PROVOLES_MOVIES_ID",
                table: "RESERVATIONS",
                column: "PROVOLES_MOVIES_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMINS");

            migrationBuilder.DropTable(
                name: "RESERVATIONS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "PROVOLES");

            migrationBuilder.DropTable(
                name: "CINEMAS");

            migrationBuilder.DropTable(
                name: "MOVIES");

            migrationBuilder.DropTable(
                name: "CONTENT_ADMIN");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
