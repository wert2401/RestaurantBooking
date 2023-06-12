﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantBooking.Data;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230612093346_RestaurantMenu")]
    partial class RestaurantMenu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("OpenFrom")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("OpenTo")
                        .HasColumnType("TEXT");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("SchemeImage")
                        .HasColumnType("TEXT");

                    b.Property<int>("TablesCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Test address",
                            Description = "Owner is owner 1",
                            Name = "Test rest",
                            OpenFrom = new TimeSpan(0, 0, 0, 0, 0),
                            OpenTo = new TimeSpan(0, 0, 0, 0, 0),
                            OwnerUserId = 1,
                            PhoneNumber = "+79991112233",
                            TablesCount = 3
                        },
                        new
                        {
                            Id = 2,
                            Address = "Test address2",
                            Description = "Owner is owner 2",
                            Name = "Test rest2",
                            OpenFrom = new TimeSpan(0, 0, 0, 0, 0),
                            OpenTo = new TimeSpan(0, 0, 0, 0, 0),
                            OwnerUserId = 2,
                            PhoneNumber = "+79991112233",
                            TablesCount = 3
                        });
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId", "RestaurantId")
                        .IsUnique();

                    b.ToTable("Review");
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Member"
                        });
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TableNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableNumber", "RestaurantId")
                        .IsUnique();

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RestaurantId = 1,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 2,
                            RestaurantId = 1,
                            TableNumber = 2
                        },
                        new
                        {
                            Id = 3,
                            RestaurantId = 1,
                            TableNumber = 3
                        },
                        new
                        {
                            Id = 4,
                            RestaurantId = 2,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 5,
                            RestaurantId = 2,
                            TableNumber = 2
                        },
                        new
                        {
                            Id = 6,
                            RestaurantId = 2,
                            TableNumber = 3
                        });
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.TableClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ClaimFromDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ClaimToDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TableId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("TableClaims");
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RefreshTokenExpiring")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "owner1@test.com",
                            Name = "Owner 1",
                            PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy",
                            Phone = "123",
                            RefreshTokenExpiring = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Email = "owner2@test.com",
                            Name = "Owner 2",
                            PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy",
                            Phone = "123",
                            RefreshTokenExpiring = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Email = "visitor@test.com",
                            Name = "Visitor 1",
                            PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy",
                            Phone = "123",
                            RefreshTokenExpiring = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RestaurantUser", b =>
                {
                    b.Property<int>("FavoriteRestaurantsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FavoritedById")
                        .HasColumnType("INTEGER");

                    b.HasKey("FavoriteRestaurantsId", "FavoritedById");

                    b.HasIndex("FavoritedById");

                    b.ToTable("RestaurantUser");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RolesId = 1,
                            UsersId = 1
                        },
                        new
                        {
                            RolesId = 1,
                            UsersId = 2
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = 3
                        });
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Review", b =>
                {
                    b.HasOne("RestaurantBooking.Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Reviews")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantBooking.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Table", b =>
                {
                    b.HasOne("RestaurantBooking.Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.TableClaim", b =>
                {
                    b.HasOne("RestaurantBooking.Data.Entities.Table", "Table")
                        .WithMany("TableClaims")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantBooking.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantUser", b =>
                {
                    b.HasOne("RestaurantBooking.Data.Entities.Restaurant", null)
                        .WithMany()
                        .HasForeignKey("FavoriteRestaurantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantBooking.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("FavoritedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("RestaurantBooking.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantBooking.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Restaurant", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("RestaurantBooking.Data.Entities.Table", b =>
                {
                    b.Navigation("TableClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
