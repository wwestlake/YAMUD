﻿// <auto-generated />
using System;
using LagDaemon.YAMUD.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    [DbContext(typeof(YamudDbContext))]
    [Migration("20240411152844_updated-inventory")]
    partial class updatedinventory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Characters.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Dexterity")
                        .HasColumnType("integer");

                    b.Property<int>("ExperiencePoints")
                        .HasColumnType("integer");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Intelligence")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Luck")
                        .HasColumnType("integer");

                    b.Property<int>("ManaPoints")
                        .HasColumnType("integer");

                    b.Property<int>("MaxHealthPoints")
                        .HasColumnType("integer");

                    b.Property<int>("MaxManaPoints")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Strength")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("PlayerStateId")
                        .HasColumnType("uuid");

                    b.Property<long>("WearAndTear")
                        .HasColumnType("bigint");

                    b.Property<long>("Weight")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerStateId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.ItemInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("ItemInstance");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Exit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Direction")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ToRoom")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Exit");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.RoomAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RoomAddress");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Scripting.CodeModule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserAccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CodeModules");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.PlayerState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrentLocationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAuthenticated")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("UserAccountID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrentLocationId");

                    b.HasIndex("UserAccountID")
                        .IsUnique();

                    b.HasIndex("UserAccountId");

                    b.ToTable("PlayerState");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserAccount", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("VerificationToken")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("InventoryId");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Utilities.Annotation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnnotatedEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("AnnotatedEntityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AnnotatedEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Annotation");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Characters.Character", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Map.RoomAddress", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.Item", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.User.PlayerState", null)
                        .WithMany("Items")
                        .HasForeignKey("PlayerStateId");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.ItemInstance", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Items.Inventory", null)
                        .WithMany("Items")
                        .HasForeignKey("InventoryId");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Exit", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Map.Room", null)
                        .WithMany("Exits")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Room", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Map.RoomAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.PlayerState", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Map.RoomAddress", "CurrentLocation")
                        .WithMany()
                        .HasForeignKey("CurrentLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LagDaemon.YAMUD.Model.User.UserAccount", null)
                        .WithOne("PlayerState")
                        .HasForeignKey("LagDaemon.YAMUD.Model.User.PlayerState", "UserAccountID");

                    b.HasOne("LagDaemon.YAMUD.Model.User.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentLocation");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserAccount", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Items.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId");

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserRole", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.User.UserAccount", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Utilities.Annotation", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Characters.Character", null)
                        .WithMany("Annotations")
                        .HasForeignKey("AnnotatedEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LagDaemon.YAMUD.Model.User.UserAccount", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Characters.Character", b =>
                {
                    b.Navigation("Annotations");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.Inventory", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Room", b =>
                {
                    b.Navigation("Exits");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.PlayerState", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserAccount", b =>
                {
                    b.Navigation("PlayerState")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
