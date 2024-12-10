using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Prj.Migrations
{
    /// <inheritdoc />
    public partial class mg06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 9, 10, 57, 19, 336, DateTimeKind.Local).AddTicks(2418), new Guid("6890a7b6-b026-45c0-9861-6c3f8a917a36") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 9, 10, 57, 19, 336, DateTimeKind.Local).AddTicks(4781));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 9, 10, 57, 19, 336, DateTimeKind.Local).AddTicks(4789));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 8, 13, 28, 18, 695, DateTimeKind.Local).AddTicks(3613), new Guid("90c6b76e-21d8-4487-a6c6-637a247ed722") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 8, 13, 28, 18, 695, DateTimeKind.Local).AddTicks(4746));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 8, 13, 28, 18, 695, DateTimeKind.Local).AddTicks(4750));
        }
    }
}
