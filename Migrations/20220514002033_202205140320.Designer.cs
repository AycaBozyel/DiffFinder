﻿// <auto-generated />
using DiffFinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiffFinder.Migrations
{
    [DbContext(typeof(MvcWebAppDbContext))]
    [Migration("20220514002033_202205140320")]
    partial class _202205140320
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("DiffFinder.Models.DiffrenceInformation", b =>
                {
                    b.Property<int>("DifferenceInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeftString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RightString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DifferenceInformationId");

                    b.ToTable("DiffrenceInformation");
                });

            modelBuilder.Entity("DiffFinder.Models.DiffsOffsets", b =>
                {
                    b.Property<int>("DiffsOffsetsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DiffrenceInformationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Diffs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Offset")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiffsOffsetsId");

                    b.HasIndex("DiffrenceInformationId");

                    b.ToTable("DiffsOffsets");
                });

            modelBuilder.Entity("DiffFinder.Models.DiffsOffsets", b =>
                {
                    b.HasOne("DiffFinder.Models.DiffrenceInformation", "DiffrenceInformation")
                        .WithMany("DiffsOffsets")
                        .HasForeignKey("DiffrenceInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiffrenceInformation");
                });

            modelBuilder.Entity("DiffFinder.Models.DiffrenceInformation", b =>
                {
                    b.Navigation("DiffsOffsets");
                });
#pragma warning restore 612, 618
        }
    }
}
