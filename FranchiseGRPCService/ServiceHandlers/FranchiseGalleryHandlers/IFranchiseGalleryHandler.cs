using Franchise;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseGalleryHandlers
{
    public interface IFranchiseGalleryHandler
    {

        // Upload a Gallery
        FranchiseGalleryResponseMessage UploadGallery(FranchiseGalleryUploadRequest request);


        // Delete a Gallery
        FranchiseGalleryResponseMessage DeleteGallery(FranchiseGalleryDeleteRequest request);
    }
}
