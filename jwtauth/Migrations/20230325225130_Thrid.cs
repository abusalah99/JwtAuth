using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class Thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordNumber",
                table: "User");

            migrationBuilder.CreateTable(
                name: "HomeSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SectionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSection", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSection");

            migrationBuilder.AddColumn<int>(
                name: "RecordNumber",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
