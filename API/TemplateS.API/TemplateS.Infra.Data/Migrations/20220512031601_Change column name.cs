using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateS.Infra.Data.Migrations
{
    public partial class Changecolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GitHubId",
                table: "PullRequests",
                newName: "Githubid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Githubid",
                table: "PullRequests",
                newName: "GitHubId");
        }
    }
}
