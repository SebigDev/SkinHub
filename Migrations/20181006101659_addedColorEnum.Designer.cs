﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkinHubApp.Data;

namespace SkinHubApp.Migrations
{
    [DbContext(typeof(SkinHubAppDbContext))]
    [Migration("20181006101659_addedColorEnum")]
    partial class addedColorEnum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SkinHubApp.Models.ColorType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenderTypeID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("GenderTypeID");

                    b.ToTable("ColorType");
                });

            modelBuilder.Entity("SkinHubApp.Models.Comment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("CommentBody");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<long>("PostID");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("SkinHubApp.Models.GenderType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("GenderType");
                });

            modelBuilder.Entity("SkinHubApp.Models.Member", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<int>("Color");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Firstname");

                    b.Property<int>("Gender");

                    b.Property<string>("Lastname");

                    b.Property<string>("Middlename");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("SkinHubApp.Models.Post", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("ProductListTypeID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("ProductListTypeID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("SkinHubApp.Models.ProductListType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ProductTypeID");

                    b.HasKey("ID");

                    b.HasIndex("ProductTypeID");

                    b.ToTable("ProductListType");
                });

            modelBuilder.Entity("SkinHubApp.Models.ProductType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorTypeID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("ColorTypeID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("SkinHubApp.Models.Reply", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<long>("CommentID");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ReplyBody");

                    b.HasKey("ID");

                    b.HasIndex("CommentID");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("SkinHubApp.Models.ColorType", b =>
                {
                    b.HasOne("SkinHubApp.Models.GenderType", "GenderType")
                        .WithMany("ColorType")
                        .HasForeignKey("GenderTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkinHubApp.Models.Comment", b =>
                {
                    b.HasOne("SkinHubApp.Models.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkinHubApp.Models.Post", b =>
                {
                    b.HasOne("SkinHubApp.Models.ProductListType", "ProductListType")
                        .WithMany()
                        .HasForeignKey("ProductListTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkinHubApp.Models.ProductListType", b =>
                {
                    b.HasOne("SkinHubApp.Models.ProductType", "ProductType")
                        .WithMany("ProductListType")
                        .HasForeignKey("ProductTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkinHubApp.Models.ProductType", b =>
                {
                    b.HasOne("SkinHubApp.Models.ColorType", "ColorType")
                        .WithMany("ProductType")
                        .HasForeignKey("ColorTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkinHubApp.Models.Reply", b =>
                {
                    b.HasOne("SkinHubApp.Models.Comment", "Comment")
                        .WithMany("Reply")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
