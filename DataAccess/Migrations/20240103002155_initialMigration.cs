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
                    description = table.Column<string>(type: "text", nullable: false)
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
                values: new object[] { new Guid("4e4ae8a2-7bb4-4e90-808d-3e9a4ebebbb2"), new DateTime(2024, 1, 3, 7, 21, 54, 982, DateTimeKind.Local).AddTicks(3068), "manager@gmail.com", new DateTime(2024, 1, 3, 7, 21, 54, 982, DateTimeKind.Local).AddTicks(3092), "Ria Sutrani", "081236733332", "$2a$12$FXJCidziFVYODbeV9mey9OY6MxTsxRB9HK6TAlEROM.SMAEVFVrCy", "Manager" });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "guid", "created_date", "email", "modified_date", "name", "no_telp", "password", "role" },
                values: new object[] { new Guid("5a4be756-d031-4dd0-866d-d8f698c75ef4"), new DateTime(2024, 1, 3, 7, 21, 54, 705, DateTimeKind.Local).AddTicks(3942), "admin@gmail.com", new DateTime(2024, 1, 3, 7, 21, 54, 705, DateTimeKind.Local).AddTicks(3958), "Admin", "081236767632", "$2a$12$JvWp06x6yfvlDCLlaIflGOZ98mhtbOB5vE.Igtgy2raDCt8Cqi9E.", "Admin" });

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
