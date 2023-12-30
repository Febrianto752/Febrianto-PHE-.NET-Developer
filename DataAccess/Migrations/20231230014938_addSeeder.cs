using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_accounts_column_guid",
                table: "vendors");

            migrationBuilder.RenameColumn(
                name: "column_guid",
                table: "vendors",
                newName: "account_guid");

            migrationBuilder.RenameIndex(
                name: "IX_vendors_column_guid",
                table: "vendors",
                newName: "IX_vendors_account_guid");

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "guid", "created_date", "email", "modified_date", "name", "no_telp", "password", "role" },
                values: new object[] { new Guid("07114b8f-db83-4f4c-9f68-941e76c9caf2"), new DateTime(2023, 12, 30, 8, 49, 37, 243, DateTimeKind.Local).AddTicks(957), "admin@gmail.com", new DateTime(2023, 12, 30, 8, 49, 37, 243, DateTimeKind.Local).AddTicks(1025), "Admin", "081236767632", "$2a$12$IlOuxaVv5n5LnCKfoD2oL.oDMZDPZDvZo2vQccoqFSdkay6z4.7Tm", "Admin" });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "guid", "created_date", "email", "modified_date", "name", "no_telp", "password", "role" },
                values: new object[] { new Guid("573726d9-0f3f-465c-9293-750f86b744b6"), new DateTime(2023, 12, 30, 8, 49, 37, 726, DateTimeKind.Local).AddTicks(6254), "manager@gmail.com", new DateTime(2023, 12, 30, 8, 49, 37, 726, DateTimeKind.Local).AddTicks(6279), "Ria Sutrani", "081236733332", "$2a$12$8fwIwrk95yc3zzKF0T6h5OuUR6PCHwmfO5vUYo/VE8/GBFLd2t90O", "Manager" });

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_accounts_account_guid",
                table: "vendors",
                column: "account_guid",
                principalTable: "accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendors_accounts_account_guid",
                table: "vendors");

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "guid",
                keyValue: new Guid("07114b8f-db83-4f4c-9f68-941e76c9caf2"));

            migrationBuilder.DeleteData(
                table: "accounts",
                keyColumn: "guid",
                keyValue: new Guid("573726d9-0f3f-465c-9293-750f86b744b6"));

            migrationBuilder.RenameColumn(
                name: "account_guid",
                table: "vendors",
                newName: "column_guid");

            migrationBuilder.RenameIndex(
                name: "IX_vendors_account_guid",
                table: "vendors",
                newName: "IX_vendors_column_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_vendors_accounts_column_guid",
                table: "vendors",
                column: "column_guid",
                principalTable: "accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
