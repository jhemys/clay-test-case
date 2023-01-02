using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoginTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoorRole_Door_DoorsId",
                table: "DoorRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoorHistory",
                table: "DoorHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Door",
                table: "Door");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "DoorHistory",
                newName: "DoorHistories");

            migrationBuilder.RenameTable(
                name: "Door",
                newName: "Doors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoorHistories",
                table: "DoorHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doors",
                table: "Doors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmployeeId",
                table: "Logins",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoorRole_Doors_DoorsId",
                table: "DoorRole",
                column: "DoorsId",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoorRole_Doors_DoorsId",
                table: "DoorRole");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doors",
                table: "Doors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoorHistories",
                table: "DoorHistories");

            migrationBuilder.RenameTable(
                name: "Doors",
                newName: "Door");

            migrationBuilder.RenameTable(
                name: "DoorHistories",
                newName: "DoorHistory");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Door",
                table: "Door",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoorHistory",
                table: "DoorHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoorRole_Door_DoorsId",
                table: "DoorRole",
                column: "DoorsId",
                principalTable: "Door",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
