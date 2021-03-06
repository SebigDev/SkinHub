﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkinHubApp.Data;

namespace SkinHubApp.Migrations
{
    [DbContext(typeof(SkinHubAppDbContext))]
    [Migration("20180908145932_init")]
    partial class init
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

            modelBuilder.Entity("SkinHubApp.Models.GenderType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("GenderType");
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

            modelBuilder.Entity("SkinHubApp.Models.ColorType", b =>
                {
                    b.HasOne("SkinHubApp.Models.GenderType", "GenderType")
                        .WithMany("ColorType")
                        .HasForeignKey("GenderTypeID")
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
#pragma warning restore 612, 618
        }
    }
}
