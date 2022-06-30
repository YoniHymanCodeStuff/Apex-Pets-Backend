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

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MainPhotoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Required_Habitat")
                        .HasColumnType("TEXT");

                    b.Property<string>("Required_License")
                        .HasColumnType("TEXT");

                    b.Property<string>("Species")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MainPhotoId");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("API.Data.Model.DeliveryAdress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .HasColumnType("TEXT");

                    b.Property<int>("houseNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("API.Data.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DeliveryTimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderTimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderedAnimalId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OrderedAnimalSpecies")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("API.Data.Model.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnimalId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("User_type");

                    b.Property<byte[]>("hash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("salt")
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");
                });

            modelBuilder.Entity("API.Data.Model.Admin", b =>
                {
                    b.HasBaseType("API.Data.Model.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.Data.Model.Customer", b =>
                {
                    b.HasBaseType("API.Data.Model.User");

                    b.Property<int?>("AdressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreditInfo")
                        .HasColumnType("TEXT");

                    b.HasIndex("AdressId");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("API.Data.Animal", b =>
                {
                    b.HasOne("API.Data.Model.Photo", "MainPhoto")
                        .WithMany()
                        .HasForeignKey("MainPhotoId");

                    b.Navigation("MainPhoto");
                });

            modelBuilder.Entity("API.Data.Model.Order", b =>
                {
                    b.HasOne("API.Data.Model.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("API.Data.Model.Photo", b =>
                {
                    b.HasOne("API.Data.Animal", null)
                        .WithMany("images")
                        .HasForeignKey("AnimalId");
                });

            modelBuilder.Entity("API.Data.Model.User", b =>
                {
                    b.HasOne("API.Data.Model.Photo", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("API.Data.Model.Customer", b =>
                {
                    b.HasOne("API.Data.Model.DeliveryAdress", "Adress")
                        .WithMany()
                        .HasForeignKey("AdressId");

                    b.Navigation("Adress");
                });

            modelBuilder.Entity("API.Data.Animal", b =>
                {
                    b.Navigation("images");
                });

            modelBuilder.Entity("API.Data.Model.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
