using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace PrioridadesApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Completada = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreadaEl = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActualizadaEl = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Prioridades",
                columns: new[] { "Id", "ActualizadaEl", "Completada", "CreadaEl", "Descripcion", "FechaVencimiento", "Nivel", "Titulo" },
                values: new object[,]
                {
                    { 1, null, false, new DateTime(2025, 10, 12, 1, 9, 35, 679, DateTimeKind.Utc).AddTicks(1043), null, null, 3, "Entregar tarea Aplicada 1" },
                    { 2, null, false, new DateTime(2025, 10, 12, 1, 9, 35, 679, DateTimeKind.Utc).AddTicks(1044), null, null, 2, "Hacer backup del proyecto" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prioridades_Nivel",
                table: "Prioridades",
                column: "Nivel");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prioridades");
        }
    }
}
