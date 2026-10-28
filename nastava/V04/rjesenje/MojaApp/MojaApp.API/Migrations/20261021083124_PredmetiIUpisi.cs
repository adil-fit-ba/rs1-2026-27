using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MojaApp.API.Migrations
{
    /// <inheritdoc />
    public partial class PredmetiIUpisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predmeti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ects = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmeti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upisi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    DatumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ocjena = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upisi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upisi_Predmeti_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Upisi_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Predmeti",
                columns: new[] { "Id", "Ects", "Naziv", "Sifra" },
                values: new object[,]
                {
                    { 1, 7, "Razvoj softvera I", "RS1" },
                    { 2, 6, "Programiranje III", "PR3" },
                    { 3, 6, "Baze podataka II", "BP2" }
                });

            migrationBuilder.InsertData(
                table: "Upisi",
                columns: new[] { "Id", "DatumUpisa", "Ocjena", "PredmetId", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1 },
                    { 2, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 1 },
                    { 3, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predmeti_Sifra",
                table: "Predmeti",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_PredmetId",
                table: "Upisi",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_StudentId_PredmetId",
                table: "Upisi",
                columns: new[] { "StudentId", "PredmetId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Upisi");

            migrationBuilder.DropTable(
                name: "Predmeti");
        }
    }
}
