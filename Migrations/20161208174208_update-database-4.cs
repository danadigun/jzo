using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Migrations
{
    public partial class updatedatabase4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "SelectedItem");

            migrationBuilder.AddColumn<int>(
                name: "OrderReferenceId",
                table: "SelectedItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderReferenceId",
                table: "SelectedItem");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "SelectedItem",
                nullable: false,
                defaultValue: 0);
        }
    }
}
