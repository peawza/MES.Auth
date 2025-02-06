using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApplicationPostgreSQLDB.Migrations.MesSystem
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mes");

            migrationBuilder.CreateTable(
                name: "mes_Department",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DepartmentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_GroupPermission",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    ScreenId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FunctionCode = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_GroupPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_Module",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModuleName_EN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ModuleName_TH = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Seq = table.Column<int>(type: "integer", nullable: false),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_Permission",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    ScreenId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FunctionCode = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_Role",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_Screen",
                schema: "mes",
                columns: table => new
                {
                    ScreenId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name_EN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name_TH = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SupportDeviceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FunctionCode = table.Column<int>(type: "integer", nullable: false),
                    ModuleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubModuleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MainMenuFlag = table.Column<int>(type: "integer", nullable: false),
                    PermissionFlag = table.Column<int>(type: "integer", nullable: false),
                    Seq = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_Screen", x => x.ScreenId);
                });

            migrationBuilder.CreateTable(
                name: "mes_ScreenFunction",
                schema: "mes",
                columns: table => new
                {
                    FunctionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FunctionCode = table.Column<int>(type: "integer", nullable: false),
                    FunctionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_ScreenFunction", x => x.FunctionId);
                });

            migrationBuilder.CreateTable(
                name: "mes_SubModule",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubModuleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubModuleName_EN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SubModuleName_TH = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Seq = table.Column<int>(type: "integer", nullable: false),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_SubModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mes_UserRoles",
                schema: "mes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(450)", nullable: false, comment: "User ID (Main Key)"),
                    RoleId = table.Column<int>(type: "integer", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mes_UserRoles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mes_Department",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_GroupPermission",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_Module",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_Permission",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_Role",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_Screen",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_ScreenFunction",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_SubModule",
                schema: "mes");

            migrationBuilder.DropTable(
                name: "mes_UserRoles",
                schema: "mes");
        }
    }
}
