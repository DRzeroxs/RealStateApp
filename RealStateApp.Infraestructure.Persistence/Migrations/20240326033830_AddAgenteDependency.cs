using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAgenteDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pprecio",
                table: "Propiedades",
                newName: "Precio");

            migrationBuilder.AddColumn<int>(
                name: "AgenteId",
                table: "Propiedades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_AgenteId",
                table: "Propiedades",
                column: "AgenteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_Agentes_AgenteId",
                table: "Propiedades",
                column: "AgenteId",
                principalTable: "Agentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_Agentes_AgenteId",
                table: "Propiedades");

            migrationBuilder.DropIndex(
                name: "IX_Propiedades_AgenteId",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "AgenteId",
                table: "Propiedades");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Propiedades",
                newName: "Pprecio");
        }
    }
}
