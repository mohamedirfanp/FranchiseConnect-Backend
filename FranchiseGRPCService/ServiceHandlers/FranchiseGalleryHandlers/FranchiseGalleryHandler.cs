using AutoMapper;
using Franchise;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.Models;
using Grpc.Core;
using System.Security.Claims;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseGalleryHandlers
{
    public class FranchiseGalleryHandler : IFranchiseGalleryHandler
    {
        private readonly FranchiseConnectContext _context;

        public FranchiseGalleryHandler(FranchiseConnectContext context)
        {
            _context = context;
        }
        
        public FranchiseGalleryResponseMessage UploadGallery(FranchiseGalleryUploadRequest request)
        {
            try
            {
                FranchiseGalleryModel galleryModel = new FranchiseGalleryModel();
                galleryModel.FranchiseId = request.FranchiseId;
                galleryModel.FranchisePhotoUrl = request.FranchisePhotoUrl;


                _context.FranchiseGalleryModel.Add(galleryModel);

                _context.SaveChanges();

                return new FranchiseGalleryResponseMessage
                {
                    Response = "Successfully Uploaded"
                };

            }
            catch(Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
  
        }

        public FranchiseGalleryResponseMessage DeleteGallery(FranchiseGalleryDeleteRequest request)
        {
            try
            { 
                var franchiseGallery = _context.FranchiseGalleryModel.Find(request.FranchiseGalleryId);

                if (franchiseGallery == null)
                {
                    return new FranchiseGalleryResponseMessage
                    {
                        Response = "Image Not Found"
                    };
                }

                _context.FranchiseGalleryModel.Remove(franchiseGallery);
                _context.SaveChanges();

                return new FranchiseGalleryResponseMessage
                {
                    Response = "Image Successfully Removed"
                };


            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }


        }

    }
}
