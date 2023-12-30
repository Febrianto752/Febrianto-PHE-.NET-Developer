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
    [Migration("20231230014938_addSeeder")]
    partial class addSeeder
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
                        .HasColumnType("longtext")
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
                            Guid = new Guid("07114b8f-db83-4f4c-9f68-941e76c9caf2"),
                            CreatedDate = new DateTime(2023, 12, 30, 8, 49, 37, 243, DateTimeKind.Local).AddTicks(957),
                            Email = "admin@gmail.com",
                            ModifiedDate = new DateTime(2023, 12, 30, 8, 49, 37, 243, DateTimeKind.Local).AddTicks(1025),
                            Name = "Admin",
                            NoTelp = "081236767632",
                            Password = "$2a$12$IlOuxaVv5n5LnCKfoD2oL.oDMZDPZDvZo2vQccoqFSdkay6z4.7Tm",
                            Role = "Admin"
                        },
                        new
                        {
                            Guid = new Guid("573726d9-0f3f-465c-9293-750f86b744b6"),
                            CreatedDate = new DateTime(2023, 12, 30, 8, 49, 37, 726, DateTimeKind.Local).AddTicks(6254),
                            Email = "manager@gmail.com",
                            ModifiedDate = new DateTime(2023, 12, 30, 8, 49, 37, 726, DateTimeKind.Local).AddTicks(6279),
                            Name = "Ria Sutrani",
                            NoTelp = "081236733332",
                            Password = "$2a$12$8fwIwrk95yc3zzKF0T6h5OuUR6PCHwmfO5vUYo/VE8/GBFLd2t90O",
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
                        .HasColumnType("longtext")
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

                    b.Property<Guid>("PorjectGuid")
                        .HasColumnType("char(36)")
                        .HasColumnName("project_guid");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<Guid>("VendorGuid")
                        .HasColumnType("char(36)")
                        .HasColumnName("vendor_guid");

                    b.HasKey("Guid");

                    b.HasIndex("PorjectGuid");

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
                        .WithMany()
                        .HasForeignKey("PorjectGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Vendor", "Vendor")
                        .WithMany()
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
#pragma warning restore 612, 618
        }
    }
}