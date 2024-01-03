﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240103002155_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("NoTelp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("no_telp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("role");

                    b.HasKey("Guid");

                    b.ToTable("accounts");

                    b.HasData(
                        new
                        {
                            Guid = new Guid("5a4be756-d031-4dd0-866d-d8f698c75ef4"),
                            CreatedDate = new DateTime(2024, 1, 3, 7, 21, 54, 705, DateTimeKind.Local).AddTicks(3942),
                            Email = "admin@gmail.com",
                            ModifiedDate = new DateTime(2024, 1, 3, 7, 21, 54, 705, DateTimeKind.Local).AddTicks(3958),
                            Name = "Admin",
                            NoTelp = "081236767632",
                            Password = "$2a$12$JvWp06x6yfvlDCLlaIflGOZ98mhtbOB5vE.Igtgy2raDCt8Cqi9E.",
                            Role = "Admin"
                        },
                        new
                        {
                            Guid = new Guid("4e4ae8a2-7bb4-4e90-808d-3e9a4ebebbb2"),
                            CreatedDate = new DateTime(2024, 1, 3, 7, 21, 54, 982, DateTimeKind.Local).AddTicks(3068),
                            Email = "manager@gmail.com",
                            ModifiedDate = new DateTime(2024, 1, 3, 7, 21, 54, 982, DateTimeKind.Local).AddTicks(3092),
                            Name = "Ria Sutrani",
                            NoTelp = "081236733332",
                            Password = "$2a$12$FXJCidziFVYODbeV9mey9OY6MxTsxRB9HK6TAlEROM.SMAEVFVrCy",
                            Role = "Manager"
                        });
                });

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.HasKey("Guid");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("Models.ProjectTender", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("char(36)")
                        .HasColumnName("project_guid");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<Guid>("VendorGuid")
                        .HasColumnType("char(36)")
                        .HasColumnName("vendor_guid");

                    b.HasKey("Guid");

                    b.HasIndex("ProjectGuid");

                    b.HasIndex("VendorGuid");

                    b.ToTable("project_tenders");
                });

            modelBuilder.Entity("Models.Vendor", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("char(36)")
                        .HasColumnName("account_guid");

                    b.Property<string>("BusinessField")
                        .HasColumnType("longtext")
                        .HasColumnName("business_field");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext")
                        .HasColumnName("profile_image");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("TypeCompany")
                        .HasColumnType("longtext")
                        .HasColumnName("type_company");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.ToTable("vendors");
                });

            modelBuilder.Entity("Models.ProjectTender", b =>
                {
                    b.HasOne("Models.Project", "Project")
                        .WithMany("ProjectTenders")
                        .HasForeignKey("ProjectGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Vendor", "Vendor")
                        .WithMany("ProjectTenders")
                        .HasForeignKey("VendorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Models.Vendor", b =>
                {
                    b.HasOne("Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Navigation("ProjectTenders");
                });

            modelBuilder.Entity("Models.Vendor", b =>
                {
                    b.Navigation("ProjectTenders");
                });
#pragma warning restore 612, 618
        }
    }
}