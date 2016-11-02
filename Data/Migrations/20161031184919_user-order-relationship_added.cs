using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class userorderrelationship_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "SelectedItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Checkout",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItem_userId",
                table: "SelectedItem",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_userId",
                table: "Checkout",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_AspNetUsers_userId",
                table: "Checkout",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedItem_AspNetUsers_userId",
                table: "SelectedItem",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_AspNetUsers_userId",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectedItem_AspNetUsers_userId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_SelectedItem_userId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_Order_userId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_userId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "SelectedItem");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Checkout");
        }
    }
}
