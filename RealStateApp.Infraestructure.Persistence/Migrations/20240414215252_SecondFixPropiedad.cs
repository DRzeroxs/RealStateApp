using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SecondFixPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritaPropiedad");

            migrationBuilder.DropTable(
                name: "ImgPropiedadPropiedad");

            migrationBuilder.DropTable(
                name: "MejorasAplicadasPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_MejorasAplicadas_PropiedadId",
                table: "MejorasAplicadas",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_ImgPropiedades_PropieadId",
                table: "ImgPropiedades",
                column: "PropieadId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritas_PropiedadId",
                table: "Favoritas",
                column: "PropiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritas_Propiedades_PropiedadId",
                table: "Favoritas",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImgPropiedades_Propiedades_PropieadId",
                table: "ImgPropiedades",
                column: "PropieadId",
                principalTable: "Propiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MejorasAplicadas_Propiedades_PropiedadId",
                table: "MejorasAplicadas",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritas_Propiedades_PropiedadId",
                table: "Favoritas");

            migrationBuilder.DropForeignKey(
                name: "FK_ImgPropiedades_Propiedades_PropieadId",
                table: "ImgPropiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_MejorasAplicadas_Propiedades_PropiedadId",
                table: "MejorasAplicadas");

            migrationBuilder.DropIndex(
                name: "IX_MejorasAplicadas_PropiedadId",
                table: "MejorasAplicadas");

            migrationBuilder.DropIndex(
                name: "IX_ImgPropiedades_PropieadId",
                table: "ImgPropiedades");

            migrationBuilder.DropIndex(
                name: "IX_Favoritas_PropiedadId",
                table: "Favoritas");

            migrationBuilder.CreateTable(
                name: "FavoritaPropiedad",
                columns: table => new
                {
                    FavoritaId = table.Column<int>(type: "int", nullable: false),
                    PropiedadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritaPropiedad", x => new { x.FavoritaId, x.PropiedadId });
                    table.ForeignKey(
                        name: "FK_FavoritaPropiedad_Favoritas_FavoritaId",
                        column: x => x.FavoritaId,
                        principalTable: "Favoritas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritaPropiedad_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImgPropiedadPropiedad",
                columns: table => new
                {
                    ImgPropiedadId = table.Column<int>(type: "int", nullable: false),
                    PropiedadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgPropiedadPropiedad", x => new { x.ImgPropiedadId, x.PropiedadId });
                    table.ForeignKey(
                        name: "FK_ImgPropiedadPropiedad_ImgPropiedades_ImgPropiedadId",
                        column: x => x.ImgPropiedadId,
                        principalTable: "ImgPropiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImgPropiedadPropiedad_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MejorasAplicadasPropiedad",
                columns: table => new
                {
                    MejorasAplicadasId = table.Column<int>(type: "int", nullable: false),
                    PropiedadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MejorasAplicadasPropiedad", x => new { x.MejorasAplicadasId, x.PropiedadId });
                    table.ForeignKey(
                        name: "FK_MejorasAplicadasPropiedad_MejorasAplicadas_MejorasAplicadasId",
                        column: x => x.MejorasAplicadasId,
                        principalTable: "MejorasAplicadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MejorasAplicadasPropiedad_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritaPropiedad_PropiedadId",
                table: "FavoritaPropiedad",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_ImgPropiedadPropiedad_PropiedadId",
                table: "ImgPropiedadPropiedad",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_MejorasAplicadasPropiedad_PropiedadId",
                table: "MejorasAplicadasPropiedad",
                column: "PropiedadId");
        }
    }
}
