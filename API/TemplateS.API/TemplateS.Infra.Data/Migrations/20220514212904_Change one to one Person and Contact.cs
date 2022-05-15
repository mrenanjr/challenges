using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateS.Infra.Data.Migrations
{
    public partial class ChangeonetoonePersonandContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Persons_PersonId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ContactId",
                table: "Persons",
                column: "ContactId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Contacts_ContactId",
                table: "Persons",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Contacts_ContactId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ContactId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Persons");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Persons_PersonId",
                table: "Contacts",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
