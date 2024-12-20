using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waste_management_system.Migrations
{
    /// <inheritdoc />
    public partial class AddCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "CapacityId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(89)",
                oldMaxLength: 89);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Observations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PickUpRequestId",
                table: "Observations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Capacities",
                columns: table => new
                {
                    CapacityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipeOfWasteId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaxKg = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacities", x => x.CapacityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CapacityId",
                table: "Vehicles",
                column: "CapacityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Capacities_CapacityId",
                table: "Vehicles",
                column: "CapacityId",
                principalTable: "Capacities",
                principalColumn: "CapacityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Capacities_CapacityId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Capacities");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CapacityId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CapacityId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "PickUpRequestId",
                table: "Observations");

            migrationBuilder.AddColumn<float>(
                name: "Capacity",
                table: "Vehicles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "UserProfiles",
                type: "nvarchar(89)",
                maxLength: 89,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "UserProfiles",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
