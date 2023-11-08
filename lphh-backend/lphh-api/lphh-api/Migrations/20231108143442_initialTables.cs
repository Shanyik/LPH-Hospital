using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lphh_api.Migrations
{
    /// <inheritdoc />
    public partial class initialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6066), new DateTime(2023, 11, 11, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6062), new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6059) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6072), new DateTime(2023, 11, 13, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6070), new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6068) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6079), new DateTime(2023, 11, 18, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6077), new DateTime(2023, 11, 13, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6075) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6032));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6035));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(5961));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6005));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 15, 34, 42, 602, DateTimeKind.Local).AddTicks(6008));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7788), new DateTime(2023, 11, 11, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7784), new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7782) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7795), new DateTime(2023, 11, 13, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7793), new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7792) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7800), new DateTime(2023, 11, 18, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7798), new DateTime(2023, 11, 13, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7752));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7758));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7604));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 8, 13, 24, 23, 711, DateTimeKind.Local).AddTicks(7669));
        }
    }
}
