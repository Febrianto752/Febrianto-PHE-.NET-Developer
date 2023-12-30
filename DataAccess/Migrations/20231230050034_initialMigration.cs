using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_telp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    business_field = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type_company = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profile_image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    account_guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.guid);
                    table.ForeignKey(
                        name: "FK_vendors_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "project_tenders",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    vendor_guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    project_guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_tenders", x => x.guid);
                    table.ForeignKey(
                        name: "FK_project_tenders_projects_project_guid",
                        column: x => x.project_guid,
                        principalTable: "projects",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_tenders_vendors_vendor_guid",
                        column: x => x.vendor_guid,
                        principalTable: "vendors",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "guid", "created_date", "email", "modified_date", "name", "no_telp", "password", "role" },
                values: new object[] { new Guid("9ff05d8f-8b22-4a88-b52d-27425dfa150e"), new DateTime(2023, 12, 30, 12, 0, 33, 545, DateTimeKind.Local).AddTicks(9283), "admin@gmail.com", new DateTime(2023, 12, 30, 12, 0, 33, 545, DateTimeKind.Local).AddTicks(9298), "Admin", "081236767632", "$2a$12$cr0PbdiSpy3ke5RfKMYF4OFCic.fcJrz2MhPlJxiQMngVSv3jNi2a", "Admin" });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "guid", "created_date", "email", "modified_date", "name", "no_telp", "password", "role" },
                values: new object[] { new Guid("c38b8bfb-649d-400a-8019-7e06c927414a"), new DateTime(2023, 12, 30, 12, 0, 34, 35, DateTimeKind.Local).AddTicks(1761), "manager@gmail.com", new DateTime(2023, 12, 30, 12, 0, 34, 35, DateTimeKind.Local).AddTicks(1784), "Ria Sutrani", "081236733332", "$2a$12$h5cKZ2E1vBh/e4yFG93Iae9pF/KR7OkLCiXFi/q3Lc4FvEnISKqF2", "Manager" });

            migrationBuilder.CreateIndex(
                name: "IX_project_tenders_project_guid",
                table: "project_tenders",
                column: "project_guid");

            migrationBuilder.CreateIndex(
                name: "IX_project_tenders_vendor_guid",
                table: "project_tenders",
                column: "vendor_guid");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_account_guid",
                table: "vendors",
                column: "account_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_tenders");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "vendors");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
