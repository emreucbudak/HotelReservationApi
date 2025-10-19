using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModelPropAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelsId",
                table: "discountLists",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "discountLists",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_discountLists_HotelsId",
                table: "discountLists",
                column: "HotelsId");

            migrationBuilder.CreateIndex(
                name: "IX_discountLists_RoomTypeId",
                table: "discountLists",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_discountLists_Hotels_HotelsId",
                table: "discountLists",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_discountLists_RoomTypes_RoomTypeId",
                table: "discountLists",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_discountLists_Hotels_HotelsId",
                table: "discountLists");

            migrationBuilder.DropForeignKey(
                name: "FK_discountLists_RoomTypes_RoomTypeId",
                table: "discountLists");

            migrationBuilder.DropIndex(
                name: "IX_discountLists_HotelsId",
                table: "discountLists");

            migrationBuilder.DropIndex(
                name: "IX_discountLists_RoomTypeId",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "HotelsId",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "discountLists");
        }
    }
}
