using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelReservationApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_priceList_PriceListId",
                table: "RoomTypes");

            migrationBuilder.DropTable(
                name: "priceList");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_PriceListId",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "PriceListId",
                table: "RoomTypes",
                newName: "DiscountedPrice");

            migrationBuilder.AddColumn<int>(
                name: "DailyPrice",
                table: "RoomTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscountForOnlyRoomTypes",
                table: "discountLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpireDate",
                table: "Coupons",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Coupons",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "IsDiscountForOnlyRoomTypes",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                table: "RoomTypes",
                newName: "PriceListId");

            migrationBuilder.CreateTable(
                name: "priceList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountListId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_priceList_discountLists_DiscountListId",
                        column: x => x.DiscountListId,
                        principalTable: "discountLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_PriceListId",
                table: "RoomTypes",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_priceList_DiscountListId",
                table: "priceList",
                column: "DiscountListId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_priceList_PriceListId",
                table: "RoomTypes",
                column: "PriceListId",
                principalTable: "priceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
