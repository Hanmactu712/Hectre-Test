using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hectre.BackEnd.Migrations
{
    public partial class initiationDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dlms_db");

            migrationBuilder.CreateTable(
                name: "Chemical",
                schema: "dlms_db",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ChemicalType = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PreHarvestIntervalInDays = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ActiveIngredient = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemical", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chemical",
                schema: "dlms_db");
        }
    }
}
