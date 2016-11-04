using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace jzo.Data.Migrations
{
    public partial class fixedidentityofferror3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_itemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_itemGroupId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemGroup",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "itemGroupId",
                table: "Item",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Item",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.RenameColumn(
                name: "itemGroupId",
                table: "Item",
                newName: "ItemGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemGroup",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "Item",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Item",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Item_itemGroupId",
                table: "Item",
                column: "ItemGroupId");

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
        }
    }
}
