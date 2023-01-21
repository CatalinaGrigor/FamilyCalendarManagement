using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpCatalinaV4.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tache",
                columns: table => new
                {
                    TacheID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duree = table.Column<double>(type: "float", nullable: false),
                    Repetitive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tache", x => x.TacheID);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    UtilisateurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.UtilisateurID);
                });

            migrationBuilder.CreateTable(
                name: "Horaire",
                columns: table => new
                {
                    HoraireID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbRepet = table.Column<int>(type: "int", nullable: false),
                    UtilisateurID = table.Column<int>(type: "int", nullable: false),
                    TacheID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horaire", x => x.HoraireID);
                    table.ForeignKey(
                        name: "FK_Horaire_Tache_TacheID",
                        column: x => x.TacheID,
                        principalTable: "Tache",
                        principalColumn: "TacheID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horaire_Utilisateur_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "Utilisateur",
                        principalColumn: "UtilisateurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horaire_TacheID",
                table: "Horaire",
                column: "TacheID");

            migrationBuilder.CreateIndex(
                name: "IX_Horaire_UtilisateurID",
                table: "Horaire",
                column: "UtilisateurID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horaire");

            migrationBuilder.DropTable(
                name: "Tache");

            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
