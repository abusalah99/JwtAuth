using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class fourteenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ImageExtention",
                table: "User",
                newName: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "User",
                newName: "ImageExtention");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserImage",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
