using Microsoft.EntityFrameworkCore.Migrations;

namespace FormToPdf.Infrastructure.Migrations
{
    public partial class AddEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailId",
                table: "Pdfs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pdfs_EmailId",
                table: "Pdfs",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pdfs_EmailTemplate_EmailId",
                table: "Pdfs",
                column: "EmailId",
                principalTable: "EmailTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pdfs_EmailTemplate_EmailId",
                table: "Pdfs");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_Pdfs_EmailId",
                table: "Pdfs");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Pdfs");
        }
    }
}
