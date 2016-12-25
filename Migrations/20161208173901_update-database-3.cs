using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Migrations
{
    public partial class updatedatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectedItem_Order_orderId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_SelectedItem_orderId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_Order_userId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "referenceId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "orderId",
                table: "SelectedItem",
                nullable: false);

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "SelectedItem",
                newName: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "referenceId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "user",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "SelectedItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItem_orderId",
                table: "SelectedItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedItem_Order_orderId",
                table: "SelectedItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "SelectedItem",
                newName: "orderId");
        }
    }
}
