using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFC.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drinksmenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "INTEGER", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinksmenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    AlcoholPercentage = table.Column<double>(type: "REAL", nullable: false),
                    ContainsUmbrella = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrinksMenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Drinksmenus_DrinksMenuId",
                        column: x => x.DrinksMenuId,
                        principalTable: "Drinksmenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DrinksMenuId",
                table: "Drinks",
                column: "DrinksMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Drinksmenus");
        }
    }
}
