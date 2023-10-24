using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lph_api.Migrations
{
    /// <inheritdoc />
    public partial class updateMigrationRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4366), new DateTime(2023, 10, 27, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4359), new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4357) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4371), new DateTime(2023, 10, 29, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4370), new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4368) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4376), new DateTime(2023, 11, 3, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4375), new DateTime(2023, 10, 29, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4373) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4337));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 13, 14, 19, 294, DateTimeKind.Local).AddTicks(4309));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdentityId",
                table: "Patients",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "IdentityId",
                table: "Doctors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
