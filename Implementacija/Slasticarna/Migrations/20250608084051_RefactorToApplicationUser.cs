using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slasticarna.Migrations
{
    /// <inheritdoc />
    public partial class RefactorToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    NacinPlacanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.NacinPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "Popust",
                columns: table => new
                {
                    PopustID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrstaProizvoda = table.Column<int>(type: "int", nullable: false),
                    IznosPopusta = table.Column<double>(type: "float", nullable: false),
                    KodZaPopust = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popust", x => x.PopustID);
                });

            migrationBuilder.CreateTable(
                name: "StanjeNarudzbe",
                columns: table => new
                {
                    StanjeNarudzbeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanjeNarudzbe", x => x.StanjeNarudzbeID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaProizvoda",
                columns: table => new
                {
                    VrstaProizvodaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaProizvoda", x => x.VrstaProizvodaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VrstaKorisnika = table.Column<int>(type: "int", nullable: false),
                    PopustID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Popust_PopustID",
                        column: x => x.PopustID,
                        principalTable: "Popust",
                        principalColumn: "PopustID");
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    VrstaProizvoda = table.Column<int>(type: "int", nullable: false),
                    PopustID = table.Column<int>(type: "int", nullable: true),
                    Ocjena = table.Column<double>(type: "float", nullable: true),
                    NutritivnaVrijednost = table.Column<int>(type: "int", nullable: true),
                    NaStanju = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ProizvodID);
                    table.ForeignKey(
                        name: "FK_Proizvod_Popust_PopustID",
                        column: x => x.PopustID,
                        principalTable: "Popust",
                        principalColumn: "PopustID");
                    table.ForeignKey(
                        name: "FK_Proizvod_VrstaProizvoda_VrstaProizvoda",
                        column: x => x.VrstaProizvoda,
                        principalTable: "VrstaProizvoda",
                        principalColumn: "VrstaProizvodaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    NarudzbaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UkupnaCijena = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vrijeme = table.Column<TimeSpan>(type: "time", nullable: false),
                    KorisnikID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StanjeNarudzbe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.NarudzbaID);
                    table.ForeignKey(
                        name: "FK_Narudzba_AppUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzba_StanjeNarudzbe_StanjeNarudzbe",
                        column: x => x.StanjeNarudzbe,
                        principalTable: "StanjeNarudzbe",
                        principalColumn: "StanjeNarudzbeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocjena",
                columns: table => new
                {
                    OcjenaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    NarudzbaID = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjena", x => x.OcjenaID);
                    table.ForeignKey(
                        name: "FK_Ocjena_AppUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocjena_Narudzba_NarudzbaID",
                        column: x => x.NarudzbaID,
                        principalTable: "Narudzba",
                        principalColumn: "NarudzbaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Placanje",
                columns: table => new
                {
                    PlacanjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PopustID = table.Column<int>(type: "int", nullable: false),
                    NarudzbaID = table.Column<int>(type: "int", nullable: false),
                    NacinPlacanjaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placanje", x => x.PlacanjeID);
                    table.ForeignKey(
                        name: "FK_Placanje_AppUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Placanje_NacinPlacanja_NacinPlacanjaID",
                        column: x => x.NacinPlacanjaID,
                        principalTable: "NacinPlacanja",
                        principalColumn: "NacinPlacanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Placanje_Narudzba_NarudzbaID",
                        column: x => x.NarudzbaID,
                        principalTable: "Narudzba",
                        principalColumn: "NarudzbaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Placanje_Popust_PopustID",
                        column: x => x.PopustID,
                        principalTable: "Popust",
                        principalColumn: "PopustID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNarudzbe",
                columns: table => new
                {
                    StavkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    NarudzbaID = table.Column<int>(type: "int", nullable: false),
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNarudzbe", x => x.StavkaID);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Narudzba_NarudzbaID",
                        column: x => x.NarudzbaID,
                        principalTable: "Narudzba",
                        principalColumn: "NarudzbaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Proizvod_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PopustID",
                table: "AppUsers",
                column: "PopustID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_StanjeNarudzbe",
                table: "Narudzba",
                column: "StanjeNarudzbe");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_KorisnikID",
                table: "Ocjena",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_NarudzbaID",
                table: "Ocjena",
                column: "NarudzbaID");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_KorisnikID",
                table: "Placanje",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_NacinPlacanjaID",
                table: "Placanje",
                column: "NacinPlacanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_NarudzbaID",
                table: "Placanje",
                column: "NarudzbaID");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_PopustID",
                table: "Placanje",
                column: "PopustID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_PopustID",
                table: "Proizvod",
                column: "PopustID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_VrstaProizvoda",
                table: "Proizvod",
                column: "VrstaProizvoda");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_NarudzbaID",
                table: "StavkeNarudzbe",
                column: "NarudzbaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_ProizvodID",
                table: "StavkeNarudzbe",
                column: "ProizvodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ocjena");

            migrationBuilder.DropTable(
                name: "Placanje");

            migrationBuilder.DropTable(
                name: "StavkeNarudzbe");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "StanjeNarudzbe");

            migrationBuilder.DropTable(
                name: "VrstaProizvoda");

            migrationBuilder.DropTable(
                name: "Popust");
        }
    }
}
