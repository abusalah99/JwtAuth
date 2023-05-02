using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class Tenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "RecordResult");

            migrationBuilder.RenameColumn(
                name: "IamgePath",
                table: "HomeSection",
                newName: "Extention");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "HomeSection",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "HomeSection");

            migrationBuilder.RenameColumn(
                name: "Extention",
                table: "HomeSection",
                newName: "IamgePath");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "RecordResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
