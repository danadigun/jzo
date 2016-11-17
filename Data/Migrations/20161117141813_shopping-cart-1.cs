using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class shoppingcart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedItem_AspNetUsers_userId",
                table: "SelectedItem");

            migrationBuilder.DropIndex(
                name: "IX_SelectedItem_userId",
                table: "SelectedItem");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "SelectedItem");

            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "SelectedItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user",
                table: "SelectedItem");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "SelectedItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedItem_userId",
                table: "SelectedItem",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedItem_AspNetUsers_userId",
                table: "SelectedItem",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
