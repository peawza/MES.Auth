using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations.System
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tb_Application",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    AppName = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "Create By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Application", x => x.AppCode);
                });

            migrationBuilder.CreateTable(
                name: "tb_Permission",
                schema: "dbo",
                columns: table => new
                {
                    PermissionCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Permission Code"),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, comment: "Permission Description"),
                    SeqNo = table.Column<int>(type: "int", nullable: false, comment: "Sequence No. for sorting display list."),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "int", nullable: false, comment: "Update By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Permission", x => x.PermissionCode);
                });

            migrationBuilder.CreateTable(
                name: "tb_Screen",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    ScreenId = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Screen Id"),
                    ImageIcon = table.Column<string>(type: "NVARCHAR(50)", nullable: false, comment: "Screen Icon Name"),
                    Path = table.Column<string>(type: "NVARCHAR(100)", nullable: false, comment: "Screen Path"),
                    SeqNo = table.Column<int>(type: "int", nullable: false, comment: "Sequence No. for sorting display list."),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "int", nullable: false, comment: "Update By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Screen", x => new { x.AppCode, x.ScreenId });
                });

            migrationBuilder.CreateTable(
                name: "tb_PermissionName",
                schema: "dbo",
                columns: table => new
                {
                    PermissionCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Permission Code"),
                    Language = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Language Code such as en-US, th-TH"),
                    Name = table.Column<string>(type: "NVARCHAR(255)", nullable: false, comment: "Screen Name of Language")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PermissionName", x => new { x.PermissionCode, x.Language });
                    table.ForeignKey(
                        name: "FK_tb_PermissionName_tb_Permission_PermissionCode",
                        column: x => x.PermissionCode,
                        principalSchema: "dbo",
                        principalTable: "tb_Permission",
                        principalColumn: "PermissionCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_MenuSetting",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false, comment: "Running Unique Record Id"),
                    MenuType = table.Column<string>(type: "CHAR(1)", nullable: false, comment: "G = Group Folder, I = Item"),
                    ParentMenuId = table.Column<int>(type: "int", nullable: true, comment: "Parent Menu Id"),
                    SeqNo = table.Column<int>(type: "int", nullable: false, comment: "Sequence No. (Re-Order every updating)"),
                    ImageIcon = table.Column<string>(type: "NVARCHAR(50)", nullable: true, comment: "Screen Icon Name"),
                    ScreenId = table.Column<string>(type: "NVARCHAR(10)", nullable: true, comment: "Screen Id"),
                    MenuURL = table.Column<string>(type: "NVARCHAR(255)", nullable: true, comment: "Link"),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "int", nullable: false, comment: "Update By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MenuSetting", x => new { x.AppCode, x.MenuId });
                    table.ForeignKey(
                        name: "FK_tb_MenuSetting_tb_Screen_AppCode_ScreenId",
                        columns: x => new { x.AppCode, x.ScreenId },
                        principalSchema: "dbo",
                        principalTable: "tb_Screen",
                        principalColumns: new[] { "AppCode", "ScreenId" });
                });

            migrationBuilder.CreateTable(
                name: "tb_ScreenName",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    ScreenId = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Screen Id"),
                    Language = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Language Code such as en-US, th-TH"),
                    Name = table.Column<string>(type: "NVARCHAR(255)", nullable: false, comment: "Menu Name of Language")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ScreenName", x => new { x.AppCode, x.ScreenId, x.Language });
                    table.ForeignKey(
                        name: "FK_tb_ScreenName_tb_Screen_AppCode_ScreenId",
                        columns: x => new { x.AppCode, x.ScreenId },
                        principalSchema: "dbo",
                        principalTable: "tb_Screen",
                        principalColumns: new[] { "AppCode", "ScreenId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_ScreenPermission",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    ScreenId = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Screen Id"),
                    PermissionCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Permission Code")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ScreenPermission", x => new { x.AppCode, x.ScreenId, x.PermissionCode });
                    table.ForeignKey(
                        name: "FK_tb_ScreenPermission_tb_Permission_PermissionCode",
                        column: x => x.PermissionCode,
                        principalSchema: "dbo",
                        principalTable: "tb_Permission",
                        principalColumn: "PermissionCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_ScreenPermission_tb_Screen_AppCode_ScreenId",
                        columns: x => new { x.AppCode, x.ScreenId },
                        principalSchema: "dbo",
                        principalTable: "tb_Screen",
                        principalColumns: new[] { "AppCode", "ScreenId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_MenuName",
                schema: "dbo",
                columns: table => new
                {
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false, comment: "Running Unique Record Id"),
                    Language = table.Column<string>(type: "NVARCHAR(10)", nullable: false, comment: "Language Code such as en-US, th-TH"),
                    Name = table.Column<string>(type: "NVARCHAR(255)", nullable: false, comment: "Menu Name of Language")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MenuName", x => new { x.AppCode, x.MenuId, x.Language });
                    table.ForeignKey(
                        name: "FK_tb_MenuName_tb_MenuSetting_AppCode_MenuId",
                        columns: x => new { x.AppCode, x.MenuId },
                        principalSchema: "dbo",
                        principalTable: "tb_MenuSetting",
                        principalColumns: new[] { "AppCode", "MenuId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_MenuSetting_AppCode_ScreenId",
                schema: "dbo",
                table: "tb_MenuSetting",
                columns: new[] { "AppCode", "ScreenId" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_ScreenPermission_PermissionCode",
                schema: "dbo",
                table: "tb_ScreenPermission",
                column: "PermissionCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Application",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_MenuName",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_PermissionName",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_ScreenName",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_ScreenPermission",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_MenuSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_Permission",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_Screen",
                schema: "dbo");
        }
    }
}
