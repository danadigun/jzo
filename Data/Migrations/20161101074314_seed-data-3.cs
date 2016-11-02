using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class seeddata3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (5,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (5,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (5,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
