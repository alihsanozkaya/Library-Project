using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebUygulamaProje1.Migrations
{
    /// <inheritdoc />
    public partial class kiralamaTablosuEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kiralamalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(type: "integer", nullable: false),
                    KitapId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kiralamalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kiralamalar_kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "kitaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kiralamalar_KitapId",
                table: "kiralamalar",
                column: "KitapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kiralamalar");
        }
    }
}
