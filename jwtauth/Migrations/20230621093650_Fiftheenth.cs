using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class Fiftheenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pdf",
                table: "RecordResult");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "HomeSection");

            migrationBuilder.RenameColumn(
                name: "Extention",
                table: "HomeSection",
                newName: "ImageId");

            migrationBuilder.AddColumn<string>(
                name: "PdfId",
                table: "RecordResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfId",
                table: "RecordResult");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "HomeSection",
                newName: "Extention");

            migrationBuilder.AddColumn<byte[]>(
                name: "Pdf",
                table: "RecordResult",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "HomeSection",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
