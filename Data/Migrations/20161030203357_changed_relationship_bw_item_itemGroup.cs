using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class changed_relationship_bw_item_itemGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_itemGroupId",
                table: "Item",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "ItemGroupId",
                table: "Item",
                newName: "itemGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item",
                newName: "IX_Item_itemGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_itemGroupId",
                table: "Item");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item",
                column: "itemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "itemGroupId",
                table: "Item",
                newName: "ItemGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_itemGroupId",
                table: "Item",
                newName: "IX_Item_ItemGroupId");
        }
    }
}
