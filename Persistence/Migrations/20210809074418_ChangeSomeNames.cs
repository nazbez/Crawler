using Microsoft.EntityFrameworkCore.Migrations;

namespace Crawler.Persistence.Migrations
{
    public partial class ChangeSomeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInSitemap",
                table: "TestResults",
                newName: "InSitemap");

            migrationBuilder.RenameColumn(
                name: "IsInHtml",
                table: "TestResults",
                newName: "InHtml");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tests",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "TestResults",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InSitemap",
                table: "TestResults",
                newName: "IsInSitemap");

            migrationBuilder.RenameColumn(
                name: "InHtml",
                table: "TestResults",
                newName: "IsInHtml");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tests",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "TestResults",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
