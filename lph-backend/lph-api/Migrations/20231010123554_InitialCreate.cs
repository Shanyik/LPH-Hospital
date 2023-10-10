using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lph_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Packing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subsitutable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Username", "Ward" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5162), "Smith@gmail.com", "John", "Smith", "Incorrect", "+3610123456", "Smithy", "a" },
                    { 2L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5174), "Doughy@gmail.com", "John", "Doe", "Incorrect", "+3620123456", "Doughy", "b" },
                    { 3L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5176), "Fizy@gmail.com", "Fizz", "Buzz", "Incorrect", "+3630123456", "Fizzy", "c" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedAt", "Description", "End", "Name", "Start" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5639), "EventDescription", new DateTime(2023, 10, 13, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5635), "Donate Blood", new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5633) },
                    { 2L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5644), "EventDescription", new DateTime(2023, 10, 15, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5642), "General Exams", new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5641) },
                    { 3L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5648), "EventDescription", new DateTime(2023, 10, 20, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5647), "Donate Blood", new DateTime(2023, 10, 15, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5646) }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CreatedAt", "DoctorId", "PatientId", "Result", "Type" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5616), 1L, 1L, "resultString", "General Exam" },
                    { 2L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5618), 2L, 3L, "resultString", "General Exam" },
                    { 3L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5621), 3L, 2L, "resultString", "General Exam" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "MedicalNumber", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4526), "Smith@gmail.com", "John", "Smith", "123-456-789", "Incorrect", "+3610123456", "Smithy" },
                    { 2L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4579), "Doughy@gmail.com", "John", "Doe", "123-456-781", "Incorrect", "+3620123456", "Doughy" },
                    { 3L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4582), "Fizy@gmail.com", "Fizz", "Buzz", "123-456-782", "Incorrect", "+3630123456", "Fizzy" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "CreatedAt", "Description", "DoctorId", "PatientId", "ProductId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5588), "1x2 for 10 days", 1L, 1L, 1L },
                    { 2L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5596), "1x1 for 120 days", 2L, 1L, 3L },
                    { 3L, new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5598), "1x1 for 30 days", 3L, 2L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Packing", "Subsitutable" },
                values: new object[,]
                {
                    { 1L, "Flector Rapid 50", "20x", true },
                    { 2L, "Drisdol", "3x10", true },
                    { 3L, "Ventolin", "1x120", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Username",
                table: "Doctors",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Username_MedicalNumber",
                table: "Patients",
                columns: new[] { "Username", "MedicalNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
