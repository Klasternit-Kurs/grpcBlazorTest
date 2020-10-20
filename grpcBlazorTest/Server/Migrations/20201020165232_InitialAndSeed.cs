using Microsoft.EntityFrameworkCore.Migrations;

namespace grpcBlazorTest.Server.Migrations
{
    public partial class InitialAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisniks",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisniks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Adresas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ulica = table.Column<string>(nullable: true),
                    Broj = table.Column<string>(nullable: true),
                    VlasnikFK = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Adresas_Korisniks_VlasnikFK",
                        column: x => x.VlasnikFK,
                        principalTable: "Korisniks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Korisniks",
                columns: new[] { "ID", "Ime", "Prezime" },
                values: new object[,]
                {
                    { "a", "Pera", "Peric" },
                    { "b", "Neko", "Nekic" },
                    { "c", "Trecko", "Treckovic" },
                    { "d", "Bla", "BlaBla" }
                });

            migrationBuilder.InsertData(
                table: "Adresas",
                columns: new[] { "ID", "Broj", "Ulica", "VlasnikFK" },
                values: new object[,]
                {
                    { -1, "1", "a", "a" },
                    { -2, "2", "b", "a" },
                    { -3, "3", "c", "a" },
                    { -4, "4", "d", "b" },
                    { -5, "5", "e", "c" },
                    { -6, "6", "f", "c" },
                    { -7, "7", "g", "d" },
                    { -8, "8", "h", "d" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresas_VlasnikFK",
                table: "Adresas",
                column: "VlasnikFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresas");

            migrationBuilder.DropTable(
                name: "Korisniks");
        }
    }
}
