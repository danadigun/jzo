using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ItemGroup (description, name) VALUES ('Tunics shirts for men', 'tunics' )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
