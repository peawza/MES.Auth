using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApplicationPostgreSQLDB.Migrations.Application
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "tb_Role",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    AppCode = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    RoleName = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR", nullable: true),
                    SystemAdminFlag = table.Column<bool>(type: "boolean", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "DATE", nullable: true, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "integer", nullable: true, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATE", nullable: true, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true, comment: "Update By"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_User",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstLoginFlag = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    SystemAdminFlag = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordAge = table.Column<int>(type: "integer", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    LastUpdatePasswordDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    ActiveDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    InActiveDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserInfo",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(450)", nullable: false, comment: "User ID (Main Key)"),
                    UserNumber = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(256)", nullable: false, comment: "UserName"),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false, comment: "First Name"),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false, comment: "Last Name"),
                    Remark = table.Column<string>(type: "VARCHAR", nullable: true, comment: "Remark, more note."),
                    LanguageCode = table.Column<string>(type: "VARCHAR", nullable: true, comment: "Language Code login"),
                    CreateDate = table.Column<DateTime>(type: "DATE", nullable: false, comment: "Create Date"),
                    CreateBy = table.Column<int>(type: "integer", nullable: false, comment: "Create By"),
                    UpdateDate = table.Column<DateTime>(type: "DATE", nullable: false, comment: "Update Date"),
                    UpdateBy = table.Column<int>(type: "integer", nullable: false, comment: "Update By")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_userlogtrail",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "UserID"),
                    create_date = table.Column<DateTime>(type: "DATE", nullable: false, comment: "Create Date"),
                    create_user = table.Column<string>(type: "VARCHAR(256)", nullable: false, comment: "Create User"),
                    action_name = table.Column<string>(type: "VARCHAR(256)", nullable: false, comment: "Action Name"),
                    data_before = table.Column<string>(type: "VARCHAR(256)", nullable: false, comment: "Data Before"),
                    data_after = table.Column<string>(type: "VARCHAR(256)", nullable: false, comment: "Data After")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tb_RoleClaim",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_RoleClaim_tb_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "tb_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_PasswordHistory",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(450)", nullable: false, comment: "User ID (Main Key)"),
                    HistoryId = table.Column<int>(type: "integer", nullable: false),
                    HistoryDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PasswordHistory", x => new { x.Id, x.HistoryId });
                    table.ForeignKey(
                        name: "FK_tb_PasswordHistory_tb_User_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserClaim",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_UserClaim_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserLogin",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tb_UserLogin_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserRole",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_UserRole_tb_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "tb_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_UserRole_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_UserToken",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tb_UserToken_tb_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "tb_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "tb_Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_RoleClaim_RoleId",
                schema: "public",
                table: "tb_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "tb_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "tb_User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserClaim_UserId",
                schema: "public",
                table: "tb_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserLogin_UserId",
                schema: "public",
                table: "tb_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_UserRole_RoleId",
                schema: "public",
                table: "tb_UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_PasswordHistory",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_RoleClaim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_UserClaim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_UserInfo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_UserLogin",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_userlogtrail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_UserRole",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_UserToken",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_Role",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_User",
                schema: "public");
        }
    }
}
