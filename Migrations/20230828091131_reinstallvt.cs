using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebUygulamaProje1.Migrations
{
    /// <inheritdoc />
    public partial class reinstallvt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kitap_turleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitap_turleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kitaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KitapAdi = table.Column<string>(type: "text", nullable: false),
                    Tanim = table.Column<string>(type: "text", nullable: false),
                    Yazar = table.Column<string>(type: "text", nullable: false),
                    Fiyat = table.Column<double>(type: "double precision", nullable: false),
                    KitapTuruId = table.Column<int>(type: "integer", nullable: false),
                    ResimUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kitaplar_kitap_turleri_KitapTuruId",
                        column: x => x.KitapTuruId,
                        principalTable: "kitap_turleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kitaplar_KitapTuruId",
                table: "kitaplar",
                column: "KitapTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kitaplar");

            migrationBuilder.DropTable(
                name: "kitap_turleri");
        }
    }
}
