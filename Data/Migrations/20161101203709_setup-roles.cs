using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Identity;

namespace jzo.Data.Migrations
{
    public partial class setuproles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (1,'Customer', 'Customer')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (2,'Administrator', 'Administrator')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
