using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations.Application
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
                name: "tb_Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppCode = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    RoleName = table.Column<string>(type: "NVARCHAR(256)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    SystemAdminFlag = table.Column<bool>(type: "bit", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: true, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: true, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "Update By"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstLoginFlag = table.Column<bool>(type: "bit", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    SystemAdminFlag = table.Column<bool>(type: "bit", nullable: false),
                    PasswordAge = table.Column<int>(type: "int", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    LastUpdatePasswordDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ActiveDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    InActiveDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(450)", nullable: false, comment: "User ID (Main Key)"),
                    UserNumber = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(256)", nullable: false, comment: "UserName"),
                    FirstName = table.Column<string>(type: "NVARCHAR(100)", nullable: false, comment: "First Name"),
                    LastName = table.Column<string>(type: "NVARCHAR(100)", nullable: false, comment: "Last Name"),
                    Remark = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, comment: "Remark, more note."),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "int", nullable: false, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "int", nullable: false, comment: "Update By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_RoleClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_RoleClaim_tb_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "tb_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_PasswordHistory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(450)", nullable: false, comment: "User ID (Main Key)"),
                    HistoryId = table.Column<int>(type: "int", nullable: false),
                    HistoryDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PasswordHistory", x => new { x.Id, x.HistoryId });
                    table.ForeignKey(
                        name: "FK_tb_PasswordHistory_tb_User_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_UserClaim_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserLogin",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tb_UserLogin_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserRole",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_UserRole_tb_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "tb_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_UserRole_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserToken",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tb_UserToken_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "tb_Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_RoleClaim_RoleId",
                schema: "dbo",
                table: "tb_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "tb_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "tb_User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserClaim_UserId",
                schema: "dbo",
                table: "tb_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserLogin_UserId",
                schema: "dbo",
                table: "tb_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserRole_RoleId",
                schema: "dbo",
                table: "tb_UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_PasswordHistory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_RoleClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_UserClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_UserInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_UserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_UserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_UserToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_User",
                schema: "dbo");
        }
    }
}
