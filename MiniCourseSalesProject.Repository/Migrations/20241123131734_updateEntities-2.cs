using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCourseSalesProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
