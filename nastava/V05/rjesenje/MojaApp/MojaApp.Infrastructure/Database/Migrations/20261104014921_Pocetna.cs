using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MojaApp.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Pocetna : Migration
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
                    Ects = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmeti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smjerovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjerovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojIndeksa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateOnly>(type: "date", nullable: true),
                    SmjerId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studenti_Smjerovi_SmjerId",
                        column: x => x.SmjerId,
                        principalTable: "Smjerovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Ocjena = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                columns: new[] { "Id", "CreatedAtUtc", "Ects", "IsDeleted", "ModifiedAtUtc", "Naziv", "Sifra" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, false, null, "Razvoj softvera I", "RS1" },
                    { 2, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, false, null, "Programiranje III", "PR3" },
                    { 3, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, false, null, "Baze podataka II", "BP2" }
                });

            migrationBuilder.InsertData(
                table: "Smjerovi",
                columns: new[] { "Id", "CreatedAtUtc", "IsDeleted", "ModifiedAtUtc", "Naziv" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, "Softverski inženjering" },
                    { 2, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, "Informacioni sistemi" }
                });

            migrationBuilder.InsertData(
                table: "Studenti",
                columns: new[] { "Id", "BrojIndeksa", "CreatedAtUtc", "DatumRodjenja", "Ime", "IsDeleted", "ModifiedAtUtc", "Prezime", "SmjerId" },
                values: new object[,]
                {
                    { 1, "IB260001", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateOnly(2006, 3, 14), "Ana", false, null, "Anić", 1 },
                    { 2, "IB260002", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Edin", false, null, "Edić", 2 }
                });

            migrationBuilder.InsertData(
                table: "Upisi",
                columns: new[] { "Id", "CreatedAtUtc", "DatumUpisa", "IsDeleted", "ModifiedAtUtc", "Ocjena", "PredmetId", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 1, 1 },
                    { 2, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 8, 2, 1 },
                    { 3, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predmeti_Sifra",
                table: "Predmeti",
                column: "Sifra",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_SmjerId",
                table: "Studenti",
                column: "SmjerId");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_PredmetId",
                table: "Upisi",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_StudentId_PredmetId",
                table: "Upisi",
                columns: new[] { "StudentId", "PredmetId" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Upisi");

            migrationBuilder.DropTable(
                name: "Predmeti");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Smjerovi");
        }
    }
}
