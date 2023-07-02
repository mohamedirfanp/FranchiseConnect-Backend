
using FranchiseGRPCService.Models;
using Microsoft.EntityFrameworkCore;

namespace FranchiseGRPCService.Data
{
    public class FranchiseConnectContext : DbContext
    {
        public FranchiseConnectContext(DbContextOptions<FranchiseConnectContext> options) : base(options)
        { }

        public DbSet<FranchiseModel> FranchiseModel { get; set; } = default!;
        public DbSet<FranchiseGalleryModel> FranchiseGalleryModel { get; set; } = default!;
        public DbSet<FranchiseRequestModel> FranchiseRequestModel { get; set; } = default!;
        public DbSet<FranchiseReviewModel> FranchiseReviewModel { get; set; } = default!;
        public DbSet<FranchiseServiceModel> FranchiseServiceModel { get; set; } = default!;
        public DbSet<FranchiseSocialModel> FranchiseSocialModel { get; set; } = default!;
        public DbSet<FranchiseCustomizedOptionModel> FranchiseCustomizedOptionModel { get; set; } = default!;
        public DbSet<FranchiseSelectedService> FranchiseSelectedServiceModel { get; set; } = default!;

        public DbSet<UserWishlistModel> UserWishlistModel { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FranchiseModel>()
                .HasMany(franchiseService =>  franchiseService.franchiseServices)
                .WithOne(franchiseId => franchiseId.franchiseId)
                .HasForeignKey(franchiseId => franchiseId.FranchiseId);

            modelBuilder.Entity<FranchiseModel>()
                 .HasMany(franchiseReviews => franchiseReviews.franchiseReviews)
                .WithOne(franchiseId => franchiseId.franchiseId)
                .HasForeignKey(franchiseId => franchiseId.FranchiseId);


            modelBuilder.Entity<FranchiseModel>()
                 .HasMany(franchiseGallery => franchiseGallery.franchiseGallery)
                .WithOne(franchiseId => franchiseId.franchiseId)
                .HasForeignKey(franchiseId => franchiseId.FranchiseId);

            modelBuilder.Entity<FranchiseCustomizedOptionModel>()
                .HasMany(franchiseSelectedService => franchiseSelectedService.FranchiseSelectedServices)
                .WithOne(franchiseCustom => franchiseCustom.FranchiseCustomizedOption)
                .HasForeignKey(franchiseCustom => franchiseCustom.FranchiseSelectedServiceId);



            

        }


    }
}
