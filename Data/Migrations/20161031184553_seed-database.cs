using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class seeddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ItemGroups (Id, description, name) VALUES (1, Tunics shirts for men, tunics )");
            migrationBuilder.Sql("INSERT INTO ItemGroups (Id, description, name) VALUES (1, Tunics shirts for men, tunics )");
            migrationBuilder.Sql("INSERT INTO ItemGroups (Id, description, name) VALUES (1, Tunics shirts for men, tunics )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
