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
    [Migration("20240325222356_updated-foreign-key-for-player-state")]
    partial class updatedforeignkeyforplayerstate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.ItemBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlayerStateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlayerStateId");

                    b.ToTable("ItemBase");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Exits", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Direction")
                        .HasColumnType("integer");

                    b.Property<Guid>("ToRoom")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Exits");
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

                    b.Property<Guid>("ExitsId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ExitsId");

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

                    b.Property<int>("Roles")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("VerificationToken")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Items.ItemBase", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.User.PlayerState", null)
                        .WithMany("Items")
                        .HasForeignKey("PlayerStateId");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.Map.Room", b =>
                {
                    b.HasOne("LagDaemon.YAMUD.Model.Map.RoomAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LagDaemon.YAMUD.Model.Map.Exits", "Exits")
                        .WithMany()
                        .HasForeignKey("ExitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Exits");
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

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.PlayerState", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LagDaemon.YAMUD.Model.User.UserAccount", b =>
                {
                    b.Navigation("PlayerState")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
