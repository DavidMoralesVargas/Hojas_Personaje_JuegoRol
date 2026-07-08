using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HojasPersonaje.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaPrincipiosCronica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronicas_Cronicas_CronicaId",
                table: "Cronicas");

            migrationBuilder.DropIndex(
                name: "IX_Cronicas_CronicaId",
                table: "Cronicas");

            migrationBuilder.DropColumn(
                name: "CronicaId",
                table: "Cronicas");

            migrationBuilder.CreateTable(
                name: "PrincipioCronica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrincipiosCronica = table.Column<string>(type: "text", nullable: true),
                    CronicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipioCronica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipioCronica_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrincipioCronica_CronicaId",
                table: "PrincipioCronica",
                column: "CronicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrincipioCronica");

            migrationBuilder.AddColumn<int>(
                name: "CronicaId",
                table: "Cronicas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_CronicaId",
                table: "Cronicas",
                column: "CronicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cronicas_Cronicas_CronicaId",
                table: "Cronicas",
                column: "CronicaId",
                principalTable: "Cronicas",
                principalColumn: "Id");
        }
    }
}
