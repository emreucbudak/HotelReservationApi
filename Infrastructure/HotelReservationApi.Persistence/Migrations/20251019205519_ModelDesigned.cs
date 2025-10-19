using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelReservationApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModelDesigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDiscountActive",
                table: "discountLists");

            migrationBuilder.RenameColumn(
                name: "IsDiscountForReservationDate",
                table: "discountLists",
                newName: "IsGlobal");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCategoryId",
                table: "discountLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayDays",
                table: "discountLists",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StayDays",
                table: "discountLists",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountCategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discountLists_DiscountCategoryId",
                table: "discountLists",
                column: "DiscountCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_discountLists_DiscountCategory_DiscountCategoryId",
                table: "discountLists",
                column: "DiscountCategoryId",
                principalTable: "DiscountCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_discountLists_DiscountCategory_DiscountCategoryId",
                table: "discountLists");

            migrationBuilder.DropTable(
                name: "DiscountCategory");

            migrationBuilder.DropIndex(
                name: "IX_discountLists_DiscountCategoryId",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "DiscountCategoryId",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "PayDays",
                table: "discountLists");

            migrationBuilder.DropColumn(
                name: "StayDays",
                table: "discountLists");

            migrationBuilder.RenameColumn(
                name: "IsGlobal",
                table: "discountLists",
                newName: "IsDiscountForReservationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscountActive",
                table: "discountLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
