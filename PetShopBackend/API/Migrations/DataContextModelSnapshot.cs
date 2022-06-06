﻿// <auto-generated />
using System;
using API.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("API.Data.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Species")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("API.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("hash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("salt")
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("API.Data.Model.Admin", b =>
                {
                    b.HasBaseType("API.Data.Model.User");

                    b.Property<string>("City")
                        .HasColumnType("TEXT")
                        .HasColumnName("Admin_City");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasColumnName("Admin_Email");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.Data.Model.Customer", b =>
                {
                    b.HasBaseType("API.Data.Model.User");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .HasColumnType("TEXT");

                    b.Property<int>("houseNumber")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
