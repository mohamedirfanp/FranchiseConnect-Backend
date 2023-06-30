﻿// <auto-generated />
using FranchiseGRPCService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    [DbContext(typeof(FranchiseConnectContext))]
    [Migration("20230629125230_new field added")]
    partial class newfieldadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseGalleryModel", b =>
                {
                    b.Property<int>("FranchiseGalleryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_gallery_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseGalleryId"), 1L, 1);

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_id");

                    b.Property<string>("FranchisePhotoUrl")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("franchise_photo_url");

                    b.HasKey("FranchiseGalleryId");

                    b.HasIndex("FranchiseId");

                    b.ToTable("FranchiseGallery", "dbo");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseModel", b =>
                {
                    b.Property<int>("FranchiseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseId"), 1L, 1);

                    b.Property<string>("FranchiseAbout")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("franchise_about");

                    b.Property<string>("FranchiseCurrentCount")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_current_count");

                    b.Property<bool>("FranchiseCustomizedOption")
                        .HasColumnType("bit")
                        .HasColumnName("franchise_customized_option");

                    b.Property<int>("FranchiseCustomizedOptionId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_customized_option_id");

                    b.Property<string>("FranchiseFee")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_fee");

                    b.Property<string>("FranchiseIndustry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_industry");

                    b.Property<string>("FranchiseInvestment")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_investment");

                    b.Property<string>("FranchiseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_name");

                    b.Property<int>("FranchiseOwnerId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_owner_id");

                    b.Property<string>("FranchisePreferredExpansionLocation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_preferred_expansion_location");

                    b.Property<bool>("FranchiseSampleBoxOption")
                        .HasColumnType("bit")
                        .HasColumnName("franchise_sample_box_option");

                    b.Property<string>("FranchiseSegment")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_segment");

                    b.Property<int>("FranchiseSocialId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_social_id");

                    b.Property<string>("FranchiseSpace")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_space");

                    b.Property<int>("FranchiseViewCount")
                        .HasColumnType("int")
                        .HasColumnName("franchise_view_count");

                    b.HasKey("FranchiseId");

                    b.HasIndex("FranchiseSocialId");

                    b.ToTable("Franchise", "dbo");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseRequestModel", b =>
                {
                    b.Property<int>("FranchiseRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_request_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseRequestId"), 1L, 1);

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_id");

                    b.Property<string>("FranchiseRequestReport")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("franchise_request_report");

                    b.Property<bool>("FranchiseSampleBoxOption")
                        .HasColumnType("bit")
                        .HasColumnName("franchise_sample_box_option");

                    b.HasKey("FranchiseRequestId");

                    b.HasIndex("FranchiseId")
                        .IsUnique();

                    b.ToTable("FranchiseRequestModel");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseReviewModel", b =>
                {
                    b.Property<int>("FranchiseReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_review_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseReviewId"), 1L, 1);

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_id");

                    b.Property<string>("FranchiseRating")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_rating");

                    b.Property<string>("FranchiseReview")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("franchise_review");

                    b.HasKey("FranchiseReviewId");

                    b.HasIndex("FranchiseId");

                    b.ToTable("FranchiseReview", "dbo");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseServiceModel", b =>
                {
                    b.Property<int>("FranchiseServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_service_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseServiceId"), 1L, 1);

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int")
                        .HasColumnName("franchise_id");

                    b.Property<string>("FranchiseProvideServiceDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("franchise_provided_service_description");

                    b.Property<string>("FranchiseProvideServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("franchise_provided_service_name");

                    b.HasKey("FranchiseServiceId");

                    b.HasIndex("FranchiseId");

                    b.ToTable("FranchiseService", "dbo");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseSocialModel", b =>
                {
                    b.Property<int>("FranchiseSocialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("franchise_social_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FranchiseSocialId"), 1L, 1);

                    b.Property<string>("FranchiseEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("franchise_email");

                    b.Property<string>("FranchiseFacebook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("franchise_facebook");

                    b.Property<string>("FranchiseInstagram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("franchise_instagram");

                    b.Property<string>("FranchiseTwitter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("franchise_twitter");

                    b.Property<string>("FranchiseWebsite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("franchise_website");

                    b.HasKey("FranchiseSocialId");

                    b.ToTable("FranchiseSocial", "dbo");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseGalleryModel", b =>
                {
                    b.HasOne("FranchiseGRPCService.Models.FranchiseModel", "franchiseId")
                        .WithMany("franchiseGallery")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("franchiseId");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseModel", b =>
                {
                    b.HasOne("FranchiseGRPCService.Models.FranchiseSocialModel", "franchiseSocialId")
                        .WithMany()
                        .HasForeignKey("FranchiseSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("franchiseSocialId");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseRequestModel", b =>
                {
                    b.HasOne("FranchiseGRPCService.Models.FranchiseModel", "franchiseId")
                        .WithOne("franchiseRequestId")
                        .HasForeignKey("FranchiseGRPCService.Models.FranchiseRequestModel", "FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("franchiseId");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseReviewModel", b =>
                {
                    b.HasOne("FranchiseGRPCService.Models.FranchiseModel", "franchiseId")
                        .WithMany("franchiseReviews")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("franchiseId");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseServiceModel", b =>
                {
                    b.HasOne("FranchiseGRPCService.Models.FranchiseModel", "franchiseId")
                        .WithMany("franchiseServices")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("franchiseId");
                });

            modelBuilder.Entity("FranchiseGRPCService.Models.FranchiseModel", b =>
                {
                    b.Navigation("franchiseGallery");

                    b.Navigation("franchiseRequestId")
                        .IsRequired();

                    b.Navigation("franchiseReviews");

                    b.Navigation("franchiseServices");
                });
#pragma warning restore 612, 618
        }
    }
}
