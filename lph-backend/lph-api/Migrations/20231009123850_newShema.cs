using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lph_api.Migrations
{
    /// <inheritdoc />
    public partial class newShema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(9490));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(9502));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(9505));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 38, 50, 231, DateTimeKind.Local).AddTicks(8911));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(2243));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(2255));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(2258));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(1645));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 14, 37, 4, 879, DateTimeKind.Local).AddTicks(1690));
        }
    }
}
