using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waste_management_system.Migrations
{
    /// <inheritdoc />
    public partial class AddWholeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    ObservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.ObservationId);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    RequestStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.RequestStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleClass = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Capacity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfWastes",
                columns: table => new
                {
                    TypeOfWasteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    VehiclesVehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfWastes", x => x.TypeOfWasteId);
                    table.ForeignKey(
                        name: "FK_TypeOfWastes_Vehicles_VehiclesVehicleId",
                        column: x => x.VehiclesVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PickUpRequests",
                columns: table => new
                {
                    PickUpRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfWasteId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    ObservationId = table.Column<int>(type: "int", nullable: true),
                    ObservationsObservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUpRequests", x => x.PickUpRequestId);
                    table.ForeignKey(
                        name: "FK_PickUpRequests_Observations_ObservationsObservationId",
                        column: x => x.ObservationsObservationId,
                        principalTable: "Observations",
                        principalColumn: "ObservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickUpRequests_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "RequestStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickUpRequests_TypeOfWastes_TypeOfWasteId",
                        column: x => x.TypeOfWasteId,
                        principalTable: "TypeOfWastes",
                        principalColumn: "TypeOfWasteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickUpRequests_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickUpRequests_ObservationsObservationId",
                table: "PickUpRequests",
                column: "ObservationsObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_PickUpRequests_RequestStatusId",
                table: "PickUpRequests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PickUpRequests_TypeOfWasteId",
                table: "PickUpRequests",
                column: "TypeOfWasteId");

            migrationBuilder.CreateIndex(
                name: "IX_PickUpRequests_UserProfileId",
                table: "PickUpRequests",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfWastes_VehiclesVehicleId",
                table: "TypeOfWastes",
                column: "VehiclesVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickUpRequests");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "TypeOfWastes");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
