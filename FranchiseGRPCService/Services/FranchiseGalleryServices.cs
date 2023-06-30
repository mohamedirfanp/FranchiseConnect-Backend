using Franchise;
using FranchiseGRPCService.ServiceHandlers.FranchiseGalleryHandlers;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class FranchiseGalleryServices : FranchiseGalleryService.FranchiseGalleryServiceBase
    {
        private readonly IFranchiseGalleryHandler _franchiseGalleryHandler;

        public FranchiseGalleryServices(IFranchiseGalleryHandler franchiseGalleryHandler)
        {
            _franchiseGalleryHandler = franchiseGalleryHandler;
        }

        public override Task<FranchiseGalleryResponseMessage> UploadFranchisePhoto(FranchiseGalleryUploadRequest request, ServerCallContext context)
        {
            var response = _franchiseGalleryHandler.UploadGallery(request);

            return Task.FromResult(response);
        }

        public override Task<FranchiseGalleryResponseMessage> DeleteFranchisePhoto(FranchiseGalleryDeleteRequest request, ServerCallContext context)
        {
            var response = _franchiseGalleryHandler.DeleteGallery(request);
            return Task.FromResult(response);
        }
    }
}
