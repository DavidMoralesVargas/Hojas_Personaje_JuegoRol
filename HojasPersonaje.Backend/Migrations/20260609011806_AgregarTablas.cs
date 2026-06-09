using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HojasPersonaje.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDepredador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDepredador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_Usuario = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Contrasena = table.Column<string>(type: "text", nullable: true),
                    Foto = table.Column<string>(type: "text", nullable: true),
                    tipoUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vampiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vampiros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadesDisciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<int>(type: "integer", nullable: false),
                    Enardecimiento = table.Column<bool>(type: "boolean", nullable: false),
                    Tirada = table.Column<string>(type: "text", nullable: true),
                    disciplinaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadesDisciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadesDisciplinas_Disciplinas_disciplinaId",
                        column: x => x.disciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cronicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NombreCronica = table.Column<string>(type: "text", nullable: true),
                    PaisCronica = table.Column<string>(type: "text", nullable: true),
                    Finalizado = table.Column<bool>(type: "boolean", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: true),
                    DungeonMasterId = table.Column<int>(type: "integer", nullable: false),
                    CronicaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cronicas_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cronicas_Usuarios_DungeonMasterId",
                        column: x => x.DungeonMasterId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebilidadesClanes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bane = table.Column<string>(type: "text", nullable: true),
                    Compulsion = table.Column<string>(type: "text", nullable: true),
                    vampiroId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebilidadesClanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebilidadesClanes_Vampiros_vampiroId",
                        column: x => x.vampiroId,
                        principalTable: "Vampiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinasVampiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vampiroId = table.Column<int>(type: "integer", nullable: false),
                    disciplinaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinasVampiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinasVampiros_Disciplinas_disciplinaId",
                        column: x => x.disciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinasVampiros_Vampiros_vampiroId",
                        column: x => x.vampiroId,
                        principalTable: "Vampiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HojasPersonajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Ambicion = table.Column<string>(type: "text", nullable: true),
                    Concepto = table.Column<string>(type: "text", nullable: true),
                    Desire = table.Column<string>(type: "text", nullable: true),
                    usuarioId = table.Column<int>(type: "integer", nullable: false),
                    CronicaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HojasPersonajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HojasPersonajes_Cronicas_CronicaId",
                        column: x => x.CronicaId,
                        principalTable: "Cronicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojasPersonajes_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtributosHojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fuerza = table.Column<int>(type: "integer", nullable: false),
                    Destreza = table.Column<int>(type: "integer", nullable: false),
                    Resistencia = table.Column<int>(type: "integer", nullable: false),
                    Carisma = table.Column<int>(type: "integer", nullable: false),
                    Manipulacion = table.Column<int>(type: "integer", nullable: false),
                    Compostura = table.Column<int>(type: "integer", nullable: false),
                    Inteligencia = table.Column<int>(type: "integer", nullable: false),
                    Astucia = table.Column<int>(type: "integer", nullable: false),
                    Resolucion = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributosHojas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtributosHojas_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Backgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Backgrounds_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biografias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EdadReal = table.Column<int>(type: "integer", nullable: false),
                    EdadAparente = table.Column<int>(type: "integer", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaMuerte = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Apariencia = table.Column<string>(type: "text", nullable: true),
                    RastosDistintivos = table.Column<string>(type: "text", nullable: true),
                    Historia = table.Column<string>(type: "text", nullable: true),
                    Resumen = table.Column<string>(type: "text", nullable: true),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biografias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biografias_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConviccionesPiedras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PiedraToken = table.Column<string>(type: "text", nullable: true),
                    Convicciones = table.Column<string>(type: "text", nullable: true),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConviccionesPiedras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConviccionesPiedras_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinasJugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    disciplinaId = table.Column<int>(type: "integer", nullable: false),
                    hojasDePersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinasJugadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinasJugadores_Disciplinas_disciplinaId",
                        column: x => x.disciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinasJugadores_HojasPersonajes_hojasDePersonajeId",
                        column: x => x.hojasDePersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciasHojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperienciaTotal = table.Column<int>(type: "integer", nullable: false),
                    ExperienciaGastada = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciasHojas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienciasHojas_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flaws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flaws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flaws_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Atletismo = table.Column<int>(type: "integer", nullable: false),
                    Pelea = table.Column<int>(type: "integer", nullable: false),
                    Crafteo = table.Column<int>(type: "integer", nullable: false),
                    Conduccion = table.Column<int>(type: "integer", nullable: false),
                    ArmasFuego = table.Column<int>(type: "integer", nullable: false),
                    Latrocinio = table.Column<int>(type: "integer", nullable: false),
                    Melee = table.Column<int>(type: "integer", nullable: false),
                    Sigilo = table.Column<int>(type: "integer", nullable: false),
                    Supervivencia = table.Column<int>(type: "integer", nullable: false),
                    AnimalKen = table.Column<int>(type: "integer", nullable: false),
                    Etiqueta = table.Column<int>(type: "integer", nullable: false),
                    Insight = table.Column<int>(type: "integer", nullable: false),
                    Intimidacion = table.Column<int>(type: "integer", nullable: false),
                    Liderazgo = table.Column<int>(type: "integer", nullable: false),
                    Actuacion = table.Column<int>(type: "integer", nullable: false),
                    Persuacion = table.Column<int>(type: "integer", nullable: false),
                    Astucia = table.Column<int>(type: "integer", nullable: false),
                    Subterfugio = table.Column<int>(type: "integer", nullable: false),
                    Academicismo = table.Column<int>(type: "integer", nullable: false),
                    Consciencia = table.Column<int>(type: "integer", nullable: false),
                    Finanzas = table.Column<int>(type: "integer", nullable: false),
                    Investigacion = table.Column<int>(type: "integer", nullable: false),
                    Medicina = table.Column<int>(type: "integer", nullable: false),
                    Ocultismo = table.Column<int>(type: "integer", nullable: false),
                    Politica = table.Column<int>(type: "integer", nullable: false),
                    Ciencia = table.Column<int>(type: "integer", nullable: false),
                    Tecnologia = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habilidades_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadesJugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hojasDePersonajeId = table.Column<int>(type: "integer", nullable: false),
                    habilidadDisciplinaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadesJugadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadesJugadores_HabilidadesDisciplinas_habilidadDiscip~",
                        column: x => x.habilidadDisciplinaId,
                        principalTable: "HabilidadesDisciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadesJugadores_HojasPersonajes_hojasDePersonajeId",
                        column: x => x.hojasDePersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HojasVampiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sire = table.Column<string>(type: "text", nullable: true),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false),
                    TipoDepredadorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HojasVampiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HojasVampiros_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojasVampiros_TiposDepredador_TipoDepredadorId",
                        column: x => x.TipoDepredadorId,
                        principalTable: "TiposDepredador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meritos_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nota = table.Column<string>(type: "text", nullable: true),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    posesion = table.Column<string>(type: "text", nullable: true),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posesiones_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    HojaPersonajeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_HojasPersonajes_HojaPersonajeId",
                        column: x => x.HojaPersonajeId,
                        principalTable: "HojasPersonajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadesHabilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Habilidad = table.Column<string>(type: "text", nullable: true),
                    Especialidad = table.Column<string>(type: "text", nullable: true),
                    habilidadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadesHabilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspecialidadesHabilidades_Habilidades_habilidadId",
                        column: x => x.habilidadId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtributosHojas_HojaPersonajeId",
                table: "AtributosHojas",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Backgrounds_HojaPersonajeId",
                table: "Backgrounds",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Biografias_HojaPersonajeId",
                table: "Biografias",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConviccionesPiedras_HojaPersonajeId",
                table: "ConviccionesPiedras",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_CronicaId",
                table: "Cronicas",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronicas_DungeonMasterId",
                table: "Cronicas",
                column: "DungeonMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DebilidadesClanes_vampiroId",
                table: "DebilidadesClanes",
                column: "vampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasJugadores_disciplinaId",
                table: "DisciplinasJugadores",
                column: "disciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasJugadores_hojasDePersonajeId",
                table: "DisciplinasJugadores",
                column: "hojasDePersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasVampiros_disciplinaId",
                table: "DisciplinasVampiros",
                column: "disciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasVampiros_vampiroId",
                table: "DisciplinasVampiros",
                column: "vampiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesHabilidades_habilidadId",
                table: "EspecialidadesHabilidades",
                column: "habilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasHojas_HojaPersonajeId",
                table: "ExperienciasHojas",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flaws_HojaPersonajeId",
                table: "Flaws",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_HojaPersonajeId",
                table: "Habilidades",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesDisciplinas_disciplinaId",
                table: "HabilidadesDisciplinas",
                column: "disciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesJugadores_habilidadDisciplinaId",
                table: "HabilidadesJugadores",
                column: "habilidadDisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesJugadores_hojasDePersonajeId",
                table: "HabilidadesJugadores",
                column: "hojasDePersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_HojasPersonajes_CronicaId",
                table: "HojasPersonajes",
                column: "CronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_HojasPersonajes_usuarioId",
                table: "HojasPersonajes",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HojasVampiros_HojaPersonajeId",
                table: "HojasVampiros",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_HojasVampiros_TipoDepredadorId",
                table: "HojasVampiros",
                column: "TipoDepredadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Meritos_HojaPersonajeId",
                table: "Meritos",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_HojaPersonajeId",
                table: "Notas",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Posesiones_HojaPersonajeId",
                table: "Posesiones",
                column: "HojaPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_HojaPersonajeId",
                table: "Weapons",
                column: "HojaPersonajeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtributosHojas");

            migrationBuilder.DropTable(
                name: "Backgrounds");

            migrationBuilder.DropTable(
                name: "Biografias");

            migrationBuilder.DropTable(
                name: "ConviccionesPiedras");

            migrationBuilder.DropTable(
                name: "DebilidadesClanes");

            migrationBuilder.DropTable(
                name: "DisciplinasJugadores");

            migrationBuilder.DropTable(
                name: "DisciplinasVampiros");

            migrationBuilder.DropTable(
                name: "EspecialidadesHabilidades");

            migrationBuilder.DropTable(
                name: "ExperienciasHojas");

            migrationBuilder.DropTable(
                name: "Flaws");

            migrationBuilder.DropTable(
                name: "HabilidadesJugadores");

            migrationBuilder.DropTable(
                name: "HojasVampiros");

            migrationBuilder.DropTable(
                name: "Meritos");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Posesiones");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Vampiros");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "HabilidadesDisciplinas");

            migrationBuilder.DropTable(
                name: "TiposDepredador");

            migrationBuilder.DropTable(
                name: "HojasPersonajes");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cronicas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
