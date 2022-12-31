using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoorHistoryAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorId",
                table: "DoorHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoorName",
                table: "DoorHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoorId",
                table: "DoorHistory");

            migrationBuilder.DropColumn(
                name: "DoorName",
                table: "DoorHistory");
        }
    }
}
