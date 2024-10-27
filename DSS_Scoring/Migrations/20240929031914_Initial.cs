using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DSS_Scoring.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Objetivo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alternativa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Alternativa_pkey", x => new { x.Id, x.IdProyecto });
                    table.UniqueConstraint("AK_Alternativa_Id", x => x.Id);
                    table.ForeignKey(
                        name: "Alternativa_IdProyecto_fkey",
                        column: x => x.IdProyecto,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Criterio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Peso = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Criterio_pkey", x => new { x.Id, x.IdProyecto });
                    table.UniqueConstraint("AK_Criterio_Id", x => x.Id);
                    table.ForeignKey(
                        name: "Criterio_IdProyecto_fkey",
                        column: x => x.IdProyecto,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdAlternativa = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Resultado_pkey", x => new { x.IdProyecto, x.IdAlternativa });
                    table.ForeignKey(
                        name: "Resultado_IdAlternativa_fkey",
                        column: x => x.IdAlternativa,
                        principalTable: "Alternativa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Resultado_IdProyecto_fkey",
                        column: x => x.IdProyecto,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Matriz",
                columns: table => new
                {
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdAlternativa = table.Column<int>(type: "integer", nullable: false),
                    IdCriterio = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Matriz_pkey", x => new { x.IdProyecto, x.IdAlternativa, x.IdCriterio });
                    table.ForeignKey(
                        name: "Matriz_IdAlternativa_fkey",
                        column: x => x.IdAlternativa,
                        principalTable: "Alternativa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Matriz_IdCriterio_fkey",
                        column: x => x.IdCriterio,
                        principalTable: "Criterio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Matriz_IdProyecto_fkey",
                        column: x => x.IdProyecto,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "Alternativa_Id_key",
                table: "Alternativa",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alternativa_IdProyecto",
                table: "Alternativa",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "Criterio_Id_key",
                table: "Criterio",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criterio_IdProyecto",
                table: "Criterio",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Matriz_IdAlternativa",
                table: "Matriz",
                column: "IdAlternativa");

            migrationBuilder.CreateIndex(
                name: "IX_Matriz_IdCriterio",
                table: "Matriz",
                column: "IdCriterio");

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_IdAlternativa",
                table: "Resultado",
                column: "IdAlternativa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriz");

            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "Criterio");

            migrationBuilder.DropTable(
                name: "Alternativa");

            migrationBuilder.DropTable(
                name: "Proyecto");
        }
    }
}
