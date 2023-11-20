using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lphh_api.Migrations
{
    /// <inheritdoc />
    public partial class admindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2871), new DateTime(2023, 11, 23, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2868), new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2866) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2877), new DateTime(2023, 11, 25, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2875), new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2881), new DateTime(2023, 11, 30, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2880), new DateTime(2023, 11, 25, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2878) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2848));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2850));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 20, 10, 37, 48, 871, DateTimeKind.Local).AddTicks(2827));

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5334), new DateTime(2023, 11, 12, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5329), new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5328) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5341), new DateTime(2023, 11, 14, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5339), new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5338) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "End", "Start" },
                values: new object[] { new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5346), new DateTime(2023, 11, 19, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5345), new DateTime(2023, 11, 14, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5343) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5308));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5311));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5284));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 9, 11, 9, 20, 790, DateTimeKind.Local).AddTicks(5286));
        }
    }
}
