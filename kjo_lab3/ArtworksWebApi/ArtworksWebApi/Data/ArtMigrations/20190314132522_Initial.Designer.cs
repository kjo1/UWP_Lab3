﻿// <auto-generated />
using System;
using ArtworksWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArtworksWebApi.Data.ArtMigrations
{
    [DbContext(typeof(ArtContext))]
    [Migration("20190314132522_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ART")
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArtworksWebApi.Models.ArtType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("ID");

                    b.ToTable("ArtTypes");
                });

            modelBuilder.Entity("ArtworksWebApi.Models.Artwork", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtTypeID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(511);

                    b.Property<DateTime>("Finished");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("Value");

                    b.HasKey("ID");

                    b.HasIndex("ArtTypeID");

                    b.HasIndex("Name", "ArtTypeID")
                        .IsUnique();

                    b.ToTable("Artworks");
                });

            modelBuilder.Entity("ArtworksWebApi.Models.Artwork", b =>
                {
                    b.HasOne("ArtworksWebApi.Models.ArtType", "ArtType")
                        .WithMany("Artworks")
                        .HasForeignKey("ArtTypeID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
