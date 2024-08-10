﻿// <auto-generated />
using System;
using CardCom.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CardCom.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240810161105_AddedRollCooldownToUser")]
    partial class AddedRollCooldownToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CardCom.Api.Models.Card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("collectionId")
                        .HasColumnType("integer");

                    b.Property<decimal>("condition")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("onSale")
                        .HasColumnType("boolean");

                    b.Property<int>("ownerId")
                        .HasColumnType("integer");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("collectionId");

                    b.HasIndex("ownerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CardCom.Api.Models.Collection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("creatorId")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("rarity")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CardCom.Api.Models.Pack", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("onSale")
                        .HasColumnType("boolean");

                    b.Property<int>("ownerId")
                        .HasColumnType("integer");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("rarity")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.HasIndex("ownerId");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("CardCom.Api.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("googleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("money")
                        .HasColumnType("integer");

                    b.Property<DateTime>("roolCooldown")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CardCom.Api.Models.Card", b =>
                {
                    b.HasOne("CardCom.Api.Models.Collection", "Collection")
                        .WithMany("Cards")
                        .HasForeignKey("collectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardCom.Api.Models.User", "Owner")
                        .WithMany("Cards")
                        .HasForeignKey("ownerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CardCom.Api.Models.Collection", b =>
                {
                    b.HasOne("CardCom.Api.Models.User", "Creator")
                        .WithMany("Collections")
                        .HasForeignKey("creatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("CardCom.Api.Models.Pack", b =>
                {
                    b.HasOne("CardCom.Api.Models.User", "Owner")
                        .WithMany("Packs")
                        .HasForeignKey("ownerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CardCom.Api.Models.Collection", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("CardCom.Api.Models.User", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Collections");

                    b.Navigation("Packs");
                });
#pragma warning restore 612, 618
        }
    }
}
