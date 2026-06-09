using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HojasPersonaje.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModificarEspecialidadHabilidadTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Habilidad",
                table: "EspecialidadesHabilidades",
                newName: "HabilidadEspecialidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HabilidadEspecialidad",
                table: "EspecialidadesHabilidades",
                newName: "Habilidad");
        }
    }
}
