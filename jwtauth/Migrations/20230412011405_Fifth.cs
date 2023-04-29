using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "HomeSection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HomeSection",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IamgePath",
                table: "HomeSection",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSection_Name",
                table: "HomeSection",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HomeSection_Name",
                table: "HomeSection");

            migrationBuilder.DropColumn(
                name: "IamgePath",
                table: "HomeSection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HomeSection",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "HomeSection",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
