using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lph_api.Migrations
{
    /// <inheritdoc />
    public partial class updateMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7229), new DateTime(2023, 10, 27, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7224), new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7223) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7234), new DateTime(2023, 10, 29, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7233), new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7231) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7239), new DateTime(2023, 11, 3, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7238), new DateTime(2023, 10, 29, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7236) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 44, 54, 379, DateTimeKind.Local).AddTicks(7179));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(432), new DateTime(2023, 10, 27, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(427), new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(425) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(437), new DateTime(2023, 10, 29, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(436), new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(434) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(443), new DateTime(2023, 11, 3, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(441), new DateTime(2023, 10, 29, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(439) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(401));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(404));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(407));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(310));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(367));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 11, 29, 0, 120, DateTimeKind.Local).AddTicks(370));
        }
    }
}
