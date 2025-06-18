using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slasticarna.Migrations
{
    public partial class PopustProizvodNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create PopustProizvodi table with PopustID as nullable for foreign key reference
            migrationBuilder.CreateTable(
                name: "PopustProizvodi",
                columns: table => new
                {
                    PopustID = table.Column<int>(type: "int", nullable: true),  // Nullable PopustID for the foreign key relationship
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    // Create a composite primary key with ProizvodID only
                    table.PrimaryKey("PK_PopustProizvodi", x => x.ProizvodID);

                    // Add foreign key constraints
                    table.ForeignKey(
                        name: "FK_PopustProizvodi_Popust_PopustID",
                        column: x => x.PopustID,
                        principalTable: "Popust",
                        principalColumn: "PopustID",
                        onDelete: ReferentialAction.SetNull);

                    table.ForeignKey(
                        name: "FK_PopustProizvodi_Proizvod_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create index for ProizvodID
            migrationBuilder.CreateIndex(
                name: "IX_PopustProizvodi_ProizvodID",
                table: "PopustProizvodi",
                column: "ProizvodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the PopustProizvodi table
            migrationBuilder.DropTable(
                name: "PopustProizvodi");
        }
    }
}
