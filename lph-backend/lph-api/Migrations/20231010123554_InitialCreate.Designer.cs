﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lph_api.Context;

#nullable disable

namespace lph_api.Migrations
{
    [DbContext(typeof(HospitalApiContext))]
    [Migration("20231010123554_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("lph_api.Model.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5162),
                            Email = "Smith@gmail.com",
                            FirstName = "John",
                            LastName = "Smith",
                            Password = "Incorrect",
                            PhoneNumber = "+3610123456",
                            Username = "Smithy",
                            Ward = "a"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5174),
                            Email = "Doughy@gmail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "Incorrect",
                            PhoneNumber = "+3620123456",
                            Username = "Doughy",
                            Ward = "b"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5176),
                            Email = "Fizy@gmail.com",
                            FirstName = "Fizz",
                            LastName = "Buzz",
                            Password = "Incorrect",
                            PhoneNumber = "+3630123456",
                            Username = "Fizzy",
                            Ward = "c"
                        });
                });

            modelBuilder.Entity("lph_api.Model.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5639),
                            Description = "EventDescription",
                            End = new DateTime(2023, 10, 13, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5635),
                            Name = "Donate Blood",
                            Start = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5633)
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5644),
                            Description = "EventDescription",
                            End = new DateTime(2023, 10, 15, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5642),
                            Name = "General Exams",
                            Start = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5641)
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5648),
                            Description = "EventDescription",
                            End = new DateTime(2023, 10, 20, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5647),
                            Name = "Donate Blood",
                            Start = new DateTime(2023, 10, 15, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5646)
                        });
                });

            modelBuilder.Entity("lph_api.Model.Exam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exams");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5616),
                            DoctorId = 1L,
                            PatientId = 1L,
                            Result = "resultString",
                            Type = "General Exam"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5618),
                            DoctorId = 2L,
                            PatientId = 3L,
                            Result = "resultString",
                            Type = "General Exam"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5621),
                            DoctorId = 3L,
                            PatientId = 2L,
                            Result = "resultString",
                            Type = "General Exam"
                        });
                });

            modelBuilder.Entity("lph_api.Model.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username", "MedicalNumber")
                        .IsUnique();

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4526),
                            Email = "Smith@gmail.com",
                            FirstName = "John",
                            LastName = "Smith",
                            MedicalNumber = "123-456-789",
                            Password = "Incorrect",
                            PhoneNumber = "+3610123456",
                            Username = "Smithy"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4579),
                            Email = "Doughy@gmail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            MedicalNumber = "123-456-781",
                            Password = "Incorrect",
                            PhoneNumber = "+3620123456",
                            Username = "Doughy"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(4582),
                            Email = "Fizy@gmail.com",
                            FirstName = "Fizz",
                            LastName = "Buzz",
                            MedicalNumber = "123-456-782",
                            Password = "Incorrect",
                            PhoneNumber = "+3630123456",
                            Username = "Fizzy"
                        });
                });

            modelBuilder.Entity("lph_api.Model.Prescription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5588),
                            Description = "1x2 for 10 days",
                            DoctorId = 1L,
                            PatientId = 1L,
                            ProductId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5596),
                            Description = "1x1 for 120 days",
                            DoctorId = 2L,
                            PatientId = 1L,
                            ProductId = 3L
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 10, 10, 14, 35, 54, 866, DateTimeKind.Local).AddTicks(5598),
                            Description = "1x1 for 30 days",
                            DoctorId = 3L,
                            PatientId = 2L,
                            ProductId = 2L
                        });
                });

            modelBuilder.Entity("lph_api.Model.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Packing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Subsitutable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Flector Rapid 50",
                            Packing = "20x",
                            Subsitutable = true
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Drisdol",
                            Packing = "3x10",
                            Subsitutable = true
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Ventolin",
                            Packing = "1x120",
                            Subsitutable = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
