using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class shoppingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedItem_Item_itemId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_SelectedItem_itemId",
                table: "SelectedItem");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "SelectedItem",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "itemId",
                table: "SelectedItem",
                nullable: false);

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "SelectedItem",
                newName: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "SelectedItem");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "SelectedItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItem_itemId",
                table: "SelectedItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedItem_Item_itemId",
                table: "SelectedItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "SelectedItem",
                newName: "itemId");
        }
    }
}
