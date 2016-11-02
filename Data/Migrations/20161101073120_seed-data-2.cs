using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jzo.Data.Migrations
{
    public partial class seeddata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ItemGroup (description, name) VALUES ('Tunics shirts for women', 'tunics black' )");

            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (1,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (1,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (1,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");

            //migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (2,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            //migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (2,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");
            //migrationBuilder.Sql("INSERT INTO Item (itemGroupId, dateCreated, description, image_url, name, price) VALUES (2,'20160801','Tunics shirts for women','//image',  'tunic item', 2500 )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
