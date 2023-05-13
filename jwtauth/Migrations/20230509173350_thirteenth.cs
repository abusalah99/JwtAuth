using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtauth.Migrations
{
    /// <inheritdoc />
    public partial class thirteenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 2, 2, 44, 52, 606, DateTimeKind.Local).AddTicks(8823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 1, 23, 44, 52, 607, DateTimeKind.Utc).AddTicks(4208));

            migrationBuilder.AddColumn<byte[]>(
                name: "Pdf",
                table: "RecordResult",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pdf",
                table: "RecordResult");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 2, 2, 44, 52, 606, DateTimeKind.Local).AddTicks(8823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 1, 23, 44, 52, 607, DateTimeKind.Utc).AddTicks(4208),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
