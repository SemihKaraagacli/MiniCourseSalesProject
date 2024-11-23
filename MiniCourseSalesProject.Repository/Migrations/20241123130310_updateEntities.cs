using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCourseSalesProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                table: "BasketItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId",
                unique: true,
                filter: "[BasketId] IS NOT NULL");

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

            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                table: "BasketItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId",
                unique: true);

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
                principalColumn: "Id");
        }
    }
}
