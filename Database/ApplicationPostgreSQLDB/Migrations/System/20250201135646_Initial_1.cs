using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationPostgreSQLDB.Migrations.System
{
    /// <inheritdoc />
    public partial class Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_LocalizedMessages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_LocalizedResources",
                schema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_LocalizedMessages",
                schema: "public",
                columns: table => new
                {
                    MessageCode = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    MessageType = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    CreateBy = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MessageNameEN = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    MessageNameTH = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Remark = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_LocalizedMessages", x => new { x.MessageCode, x.MessageType });
                });

            migrationBuilder.CreateTable(
                name: "tb_LocalizedResources",
                schema: "public",
                columns: table => new
                {
                    ScreenCode = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    ObjectID = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CreateBy = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remark = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    ResourcesEN = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    ResourcesTH = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_LocalizedResources", x => new { x.ScreenCode, x.ObjectID });
                });
        }
    }
}
