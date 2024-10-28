using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DSS_Scoring.Migrations
{
    /// <inheritdoc />
    public partial class SupabaseInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fase_proceso",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_facilitador = table.Column<int>(type: "integer", nullable: true),
                    etapa = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    activa = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("fase_proceso_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fase_proceso_id_facilitador_fkey",
                        column: x => x.id_facilitador,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "proyecto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    objetivo = table.Column<string>(type: "text", nullable: false),
                    id_facilitador = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("proyecto_pkey", x => x.id);
                    table.ForeignKey(
                        name: "proyecto_id_facilitador_fkey",
                        column: x => x.id_facilitador,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "alternativa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("alternativa_pkey", x => x.id);
                    table.ForeignKey(
                        name: "alternativa_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    mensaje = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_pkey", x => x.id);
                    table.ForeignKey(
                        name: "chat_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "criterio",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("criterio_pkey", x => x.id);
                    table.ForeignKey(
                        name: "criterio_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lluvia_ideas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    etapa = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    idea = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lluvia_ideas_pkey", x => x.id);
                    table.ForeignKey(
                        name: "lluvia_ideas_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "lluvia_ideas_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "proyecto_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("proyecto_usuario_pkey", x => x.id);
                    table.ForeignKey(
                        name: "proyecto_usuario_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "proyecto_usuario_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alternativas_finales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_alternativa = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("alternativas_finales_pkey", x => x.id);
                    table.ForeignKey(
                        name: "alternativas_finales_id_alternativa_fkey",
                        column: x => x.id_alternativa,
                        principalTable: "alternativa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "alternativas_finales_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categorizacion_alternativas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_alternativa = table.Column<int>(type: "integer", nullable: true),
                    modificado = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categorizacion_alternativas_pkey", x => x.id);
                    table.ForeignKey(
                        name: "categorizacion_alternativas_id_alternativa_fkey",
                        column: x => x.id_alternativa,
                        principalTable: "alternativa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "categorizacion_alternativas_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votacion_alternativa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_alternativa = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    voto = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("votacion_alternativa_pkey", x => x.id);
                    table.ForeignKey(
                        name: "votacion_alternativa_id_alternativa_fkey",
                        column: x => x.id_alternativa,
                        principalTable: "alternativa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_alternativa_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_alternativa_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "categorizacion_criterios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_criterio = table.Column<int>(type: "integer", nullable: true),
                    modificado = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categorizacion_criterios_pkey", x => x.id);
                    table.ForeignKey(
                        name: "categorizacion_criterios_id_criterio_fkey",
                        column: x => x.id_criterio,
                        principalTable: "criterio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "categorizacion_criterios_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "criterios_finales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_criterio = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("criterios_finales_pkey", x => x.id);
                    table.ForeignKey(
                        name: "criterios_finales_id_criterio_fkey",
                        column: x => x.id_criterio,
                        principalTable: "criterio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "criterios_finales_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peso_final",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_criterio = table.Column<int>(type: "integer", nullable: true),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    peso = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("peso_final_pkey", x => x.id);
                    table.ForeignKey(
                        name: "peso_final_id_criterio_fkey",
                        column: x => x.id_criterio,
                        principalTable: "criterio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "peso_final_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peso_propuesto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_criterio = table.Column<int>(type: "integer", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("peso_propuesto_pkey", x => x.id);
                    table.ForeignKey(
                        name: "peso_propuesto_id_criterio_fkey",
                        column: x => x.id_criterio,
                        principalTable: "criterio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votacion_criterio",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_criterio = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    voto = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("votacion_criterio_pkey", x => x.id);
                    table.ForeignKey(
                        name: "votacion_criterio_id_criterio_fkey",
                        column: x => x.id_criterio,
                        principalTable: "criterio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_criterio_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_criterio_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "votacion_peso",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_proyecto = table.Column<int>(type: "integer", nullable: true),
                    id_peso_propuesto = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    voto = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("votacion_peso_pkey", x => x.id);
                    table.ForeignKey(
                        name: "votacion_peso_id_peso_propuesto_fkey",
                        column: x => x.id_peso_propuesto,
                        principalTable: "peso_propuesto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_peso_id_proyecto_fkey",
                        column: x => x.id_proyecto,
                        principalTable: "proyecto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "votacion_peso_id_usuario_fkey",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alternativa_id_proyecto",
                table: "alternativa",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_alternativas_finales_id_alternativa",
                table: "alternativas_finales",
                column: "id_alternativa");

            migrationBuilder.CreateIndex(
                name: "IX_alternativas_finales_id_proyecto",
                table: "alternativas_finales",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_categorizacion_alternativas_id_alternativa",
                table: "categorizacion_alternativas",
                column: "id_alternativa");

            migrationBuilder.CreateIndex(
                name: "IX_categorizacion_alternativas_id_proyecto",
                table: "categorizacion_alternativas",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_categorizacion_criterios_id_criterio",
                table: "categorizacion_criterios",
                column: "id_criterio");

            migrationBuilder.CreateIndex(
                name: "IX_categorizacion_criterios_id_proyecto",
                table: "categorizacion_criterios",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_chat_id_proyecto",
                table: "chat",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_chat_id_usuario",
                table: "chat",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_criterio_id_proyecto",
                table: "criterio",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_criterios_finales_id_criterio",
                table: "criterios_finales",
                column: "id_criterio");

            migrationBuilder.CreateIndex(
                name: "IX_criterios_finales_id_proyecto",
                table: "criterios_finales",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_fase_proceso_id_facilitador",
                table: "fase_proceso",
                column: "id_facilitador");

            migrationBuilder.CreateIndex(
                name: "IX_lluvia_ideas_id_proyecto",
                table: "lluvia_ideas",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_lluvia_ideas_id_usuario",
                table: "lluvia_ideas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_peso_final_id_criterio",
                table: "peso_final",
                column: "id_criterio");

            migrationBuilder.CreateIndex(
                name: "IX_peso_final_id_proyecto",
                table: "peso_final",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_peso_propuesto_id_criterio",
                table: "peso_propuesto",
                column: "id_criterio");

            migrationBuilder.CreateIndex(
                name: "IX_proyecto_id_facilitador",
                table: "proyecto",
                column: "id_facilitador");

            migrationBuilder.CreateIndex(
                name: "IX_proyecto_usuario_id_proyecto",
                table: "proyecto_usuario",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_proyecto_usuario_id_usuario",
                table: "proyecto_usuario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "usuario_email_key",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_votacion_alternativa_id_alternativa",
                table: "votacion_alternativa",
                column: "id_alternativa");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_alternativa_id_proyecto",
                table: "votacion_alternativa",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_alternativa_id_usuario",
                table: "votacion_alternativa",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_criterio_id_criterio",
                table: "votacion_criterio",
                column: "id_criterio");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_criterio_id_proyecto",
                table: "votacion_criterio",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_criterio_id_usuario",
                table: "votacion_criterio",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_peso_id_peso_propuesto",
                table: "votacion_peso",
                column: "id_peso_propuesto");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_peso_id_proyecto",
                table: "votacion_peso",
                column: "id_proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_votacion_peso_id_usuario",
                table: "votacion_peso",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alternativas_finales");

            migrationBuilder.DropTable(
                name: "categorizacion_alternativas");

            migrationBuilder.DropTable(
                name: "categorizacion_criterios");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "criterios_finales");

            migrationBuilder.DropTable(
                name: "fase_proceso");

            migrationBuilder.DropTable(
                name: "lluvia_ideas");

            migrationBuilder.DropTable(
                name: "peso_final");

            migrationBuilder.DropTable(
                name: "proyecto_usuario");

            migrationBuilder.DropTable(
                name: "votacion_alternativa");

            migrationBuilder.DropTable(
                name: "votacion_criterio");

            migrationBuilder.DropTable(
                name: "votacion_peso");

            migrationBuilder.DropTable(
                name: "alternativa");

            migrationBuilder.DropTable(
                name: "peso_propuesto");

            migrationBuilder.DropTable(
                name: "criterio");

            migrationBuilder.DropTable(
                name: "proyecto");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
