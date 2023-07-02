using ApiGateway.Dto_Models;
using ApiGateway.GRPCMicroserviceClients;
using Franchise;
using FranchiseGRPCService.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiGateway.Services.FranchiseService
{
    public class FranchiseServiceApi : IFranchiseService
    {
        private readonly IGRPCClients _grpcClients;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FranchiseServiceApi(IGRPCClients grpcClients, IHttpContextAccessor httpContextAccessor) 
        {
           _grpcClients = grpcClients;
            _httpContextAccessor = httpContextAccessor;
        }

        // To Get the Current User ID
        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }
        // Franchise Service
        public CreateFranchiseResponse CreateFranchise(CreateFranchiseDto request)
        {
			try
			{
				CreateFranchiseRequest newFranchise = new CreateFranchiseRequest()
				{
					Franchise = new Franchise.Franchise()
					{
						FranchiseName = request.Franchise.FranchiseName,
						FranchiseAbout = request.Franchise.FranchiseAbout,
						FranchiseCurrentCount = request.Franchise.FranchiseCurrentCount,
						FranchiseCustomizedOption = request.Franchise.FranchiseCustomizedOption,
						FranchiseFee = request.Franchise.FranchiseFee,
						FranchiseIndustry = request.Franchise.FranchiseIndustry,
						FranchiseInvestment = request.Franchise.FranchiseInvestment,
						FranchisePreferredExpansionLocation = request.Franchise.FranchisePreferredExpansionLocation,
						FranchiseSegment = request.Franchise.FranchiseSpace,
						FranchiseSpace = request.Franchise.FranchiseSpace,
						FranchiseSampleBoxOption = request.Franchise.FranchiseSampleBoxOption,
						FranchiseViewCount = request.Franchise.FranchiseViewCount
					},
					FrachiseSocial = request.FranchiseSocialRequest
				};

				foreach (var gallery in request.GalleryRequestList)
				{
					newFranchise.FranchiseGalleryList.Add(new FranchiseGalleryRequest()
					{
						FranchisePhotoUrl = gallery
					});
				}

				foreach (var service in request.FranchiseServiceRequestsList)
				{
					newFranchise.FranchiseServiceList.Add(new FranchiseServiceModelRequest()
					{
						FranchiseProvideServiceName = service.FranchiseProvideServiceName,
						FranchiseProvideServiceDescription =  service.FranchiseProvideServiceDescription
					});
		 
				}
				var response = _grpcClients.FranchiseClient.CreateFranchise(newFranchise);
				return response;

			}
			catch (RpcException ex)
			{
                throw new RpcException(new Status(Grpc.Core.StatusCode.Unimplemented, ex.Message));
            }
			
            
        }

        public GetFranchiseResponse GetAllFranchise()
        {
            try
            {

                var response = _grpcClients.FranchiseClient.GetAllFranchise(new Google.Protobuf.WellKnownTypes.Empty { });

                return response;

            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(Grpc.Core.StatusCode.Unimplemented, ex.Message));
            }
        }

        public GetFranchisesRespsonse GetFranchiseById(int id)
        {

            try
            {
               
                var response = _grpcClients.FranchiseClient.GetFranchiseById(new GetFranchiseByIdRequest { FranchiseId = id });


                return response;

            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(Grpc.Core.StatusCode.Unimplemented, ex.Message));
            }
        }


        // Franchise Gallery Service
        public IActionResult UploadGallery(FranchiseGalleryUploadRequest uploadRequest)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7290");

            try
            {
                var response = _grpcClients.FranchiseGalleryClient.UploadFranchisePhoto(uploadRequest);

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        public IActionResult DeleteGallery(FranchiseGalleryDeleteRequest deleteRequest)
        {
            try
            {
                var response = _grpcClients.FranchiseGalleryClient.DeleteFranchisePhoto(deleteRequest);

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // Franchise Provide Service
        public IActionResult CreateProvideService(CreateFranchiseServiceRequest serviceRequest)
        {
            try
            {

                var response = _grpcClients.FranchiseProvideServiceClient.CreateFranchiseService(serviceRequest);

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public IActionResult DeleteProvidedService(int franchise_provide_service_id)
        {
            try
            {
               
                var response = _grpcClients.FranchiseProvideServiceClient.DeleteFranchiseService(new DeleteFranchiseServiceRequest()
                {
                    FranchiseProvideServiceId = franchise_provide_service_id
                });

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // Franchise Request Service
        public IActionResult CreateUserRequest(CreateFranchiseUserRequest franchiseUserRequest)
        {
            try
            {
                var response = _grpcClients.FranchiseRequestClient.CreateFranchiseRequest(franchiseUserRequest);
                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public FranchiseRequestResponseList GetAllFranchiseRequest()
        {
            try
            {
                var response = _grpcClients.FranchiseRequestClient.GetFranchiseRequest(new GetFranchiseRequestByUserID
                {
                    UserId = GetCurrentUserId()
                });

                return response;

            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public IActionResult UpdateRequestStatus(UpdateStatusRequest statusRequest)
        {
            try
            {
                var response = _grpcClients.FranchiseRequestClient.UpdateStatus(statusRequest);
                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // User Wishlist Service
        public IActionResult AddUserWishlist(AddUserWishListRequest userWishListRequest)
        {
            try
            {
                var response = _grpcClients.UserWishListServiceClient.AddUserWishList(userWishListRequest); 
                return new OkObjectResult(response);
            }
            catch(RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public GetUserWishListResponse GetAllWishlist(GetUserWishListRequest wishListRequest)
        {
            try
            {
                var response = _grpcClients.UserWishListServiceClient.GetUserWishList(wishListRequest);

                return response;
            }
            catch (RpcException ex)
            {
                return new GetUserWishListResponse();
            }
        }

        public IActionResult RemoveUserWishlist(RemoveUserWishListRequest removeUserWishRequest)
        {
            try
            {
                var response = _grpcClients.UserWishListServiceClient.RemoveUserWishList(removeUserWishRequest);
                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
