using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slasticarna.Migrations
{
    /// <inheritdoc />
    public partial class AddPraznicnaPonuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PraznicnaPonuda",
                columns: table => new
                {
                    PraznicnaPonudaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PraznicnaPonuda", x => x.PraznicnaPonudaID);
                });

            migrationBuilder.CreateTable(
                name: "PraznicnaPonudaProizvodi",
                columns: table => new
                {
                    PraznicnaPonudaID = table.Column<int>(type: "int", nullable: false),
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PraznicnaPonudaProizvodi", x => new { x.PraznicnaPonudaID, x.ProizvodID });
                    table.ForeignKey(
                        name: "FK_PraznicnaPonudaProizvodi_PraznicnaPonuda_PraznicnaPonudaID",
                        column: x => x.PraznicnaPonudaID,
                        principalTable: "PraznicnaPonuda",
                        principalColumn: "PraznicnaPonudaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PraznicnaPonudaProizvodi_Proizvod_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PraznicnaPonudaProizvodi_ProizvodID",
                table: "PraznicnaPonudaProizvodi",
                column: "ProizvodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PraznicnaPonudaProizvodi");

            migrationBuilder.DropTable(
                name: "PraznicnaPonuda");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Proizvod");
        }
    }
}
