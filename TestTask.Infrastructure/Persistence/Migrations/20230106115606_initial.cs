using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("50634dda-79b7-49ec-9152-249b5a265096"), "RAM DDR4 16GB", 80.319999999999993, 47 },
                    { new Guid("6637533b-5b83-459e-9af0-ad845623c82a"), "HDD 1TB", 74.090000000000003, 55 },
                    { new Guid("c50b7f72-cecb-4d50-a14b-cf354d8d00ea"), "HDD SSD 512GB", 190.99000000000001, 102 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("5378659e-9248-48dc-93f9-942748a14da3"), "U+CMicHhIuvh3W73vtQ1LE9SIr0WweXjdnfHam8if+g=", "Admin", "Test1" },
                    { new Guid("d1b89e0b-548d-45bd-9309-ac9ddd30f740"), "U+CMicHhIuvh3W73vtQ1LE9SIr0WweXjdnfHam8if+g=", "User", "Test2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
