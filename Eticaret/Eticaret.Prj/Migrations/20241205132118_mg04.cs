using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Prj.Migrations
{
    /// <inheritdoc />
    public partial class mg04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 5, 16, 21, 17, 932, DateTimeKind.Local).AddTicks(9419), new Guid("164916f8-9259-4cd8-8823-bc9c7876a88c") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 16, 21, 17, 933, DateTimeKind.Local).AddTicks(562));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 16, 21, 17, 933, DateTimeKind.Local).AddTicks(566));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 5, 15, 11, 41, 556, DateTimeKind.Local).AddTicks(8207), new Guid("888bb4dd-c373-4416-a60d-6ffdd223127f") });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreateDate", "Description", "IsActive", "Logo", "Name", "OrderNo" },
                values: new object[] { 1, new DateTime(2024, 12, 5, 15, 11, 41, 557, DateTimeKind.Local).AddTicks(3737), "Brand1 Description", true, "brand1.jpg", "Brand1", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 15, 11, 41, 556, DateTimeKind.Local).AddTicks(9288));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 15, 11, 41, 556, DateTimeKind.Local).AddTicks(9293));
        }
    }
}
