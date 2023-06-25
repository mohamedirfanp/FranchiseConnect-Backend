
using FranchiseGRPCService.Models;
using Microsoft.EntityFrameworkCore;

namespace FranchiseGRPCService.Data
{
    public class FranchiseConnectContext : DbContext
    {
        public FranchiseConnectContext(DbContextOptions<FranchiseConnectContext> options) : base(options)
        { }

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

            

        }


    }
}
