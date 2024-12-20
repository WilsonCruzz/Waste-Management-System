using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waste_management_system.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickUpRequests_Observations_ObservationsObservationId",
                table: "PickUpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfWastes_Vehicles_VehiclesVehicleId",
                table: "TypeOfWastes");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfWastes_VehiclesVehicleId",
                table: "TypeOfWastes");

            migrationBuilder.DropIndex(
                name: "IX_PickUpRequests_ObservationsObservationId",
                table: "PickUpRequests");

            migrationBuilder.DropColumn(
                name: "VehiclesVehicleId",
                table: "TypeOfWastes");

            migrationBuilder.DropColumn(
                name: "ObservationId",
                table: "PickUpRequests");

            migrationBuilder.DropColumn(
                name: "ObservationsObservationId",
                table: "PickUpRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeOfWastes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TypeOfWastes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfWastes_VehicleId",
                table: "TypeOfWastes",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_PickUpRequestId",
                table: "Observations",
                column: "PickUpRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_PickUpRequests_PickUpRequestId",
                table: "Observations",
                column: "PickUpRequestId",
                principalTable: "PickUpRequests",
                principalColumn: "PickUpRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfWastes_Vehicles_VehicleId",
                table: "TypeOfWastes",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_PickUpRequests_PickUpRequestId",
                table: "Observations");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfWastes_Vehicles_VehicleId",
                table: "TypeOfWastes");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfWastes_VehicleId",
                table: "TypeOfWastes");

            migrationBuilder.DropIndex(
                name: "IX_Observations_PickUpRequestId",
                table: "Observations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeOfWastes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TypeOfWastes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "VehiclesVehicleId",
                table: "TypeOfWastes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObservationId",
                table: "PickUpRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObservationsObservationId",
                table: "PickUpRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfWastes_VehiclesVehicleId",
                table: "TypeOfWastes",
                column: "VehiclesVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PickUpRequests_ObservationsObservationId",
                table: "PickUpRequests",
                column: "ObservationsObservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PickUpRequests_Observations_ObservationsObservationId",
                table: "PickUpRequests",
                column: "ObservationsObservationId",
                principalTable: "Observations",
                principalColumn: "ObservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfWastes_Vehicles_VehiclesVehicleId",
                table: "TypeOfWastes",
                column: "VehiclesVehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
