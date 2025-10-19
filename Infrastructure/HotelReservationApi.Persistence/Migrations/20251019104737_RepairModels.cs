using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RepairModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_priceList_PriceListId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PriceListId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "PriceListId",
                table: "RoomTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_PriceListId",
                table: "RoomTypes",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_MemberId",
                table: "reservations",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_Member_MemberId",
                table: "reservations",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_priceList_PriceListId",
                table: "RoomTypes",
                column: "PriceListId",
                principalTable: "priceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_Member_MemberId",
                table: "reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_priceList_PriceListId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_PriceListId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_reservations_MemberId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "reservations");

            migrationBuilder.AddColumn<int>(
                name: "PriceListId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PriceListId",
                table: "Rooms",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_priceList_PriceListId",
                table: "Rooms",
                column: "PriceListId",
                principalTable: "priceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
