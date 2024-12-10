using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Prj.Migrations
{
    /// <inheritdoc />
    public partial class mg01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 5, 13, 25, 11, 62, DateTimeKind.Local).AddTicks(7664), new Guid("d0e04037-8ce3-4f66-9edd-4a48ba5927be") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 13, 25, 11, 62, DateTimeKind.Local).AddTicks(8949));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 5, 13, 25, 11, 62, DateTimeKind.Local).AddTicks(8953));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 3, 15, 47, 8, 610, DateTimeKind.Local).AddTicks(3768), new Guid("81daac79-4678-447e-8d75-abbac9c37592") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 3, 15, 47, 8, 610, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 3, 15, 47, 8, 610, DateTimeKind.Local).AddTicks(6040));
        }
    }
}
