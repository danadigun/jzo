using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class applicationuserdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "state",
                table: "AspNetUsers");
        }
    }
}
