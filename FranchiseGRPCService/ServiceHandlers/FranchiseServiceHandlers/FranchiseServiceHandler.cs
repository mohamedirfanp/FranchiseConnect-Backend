using AutoMapper;
using Franchise;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.Models;
using FranchiseGRPCService.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers
{
	public class FranchiseServiceHandler : IFranchiseServiceHandler
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly FranchiseConnectContext _context;
        private readonly IMapper _mapper;

        public FranchiseServiceHandler(FranchiseConnectContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_httpContextAccessor = httpContextAccessor;
			_context = context;
			_mapper = mapper;
		}
		// To Get the Current User ID
		public int GetCurrentUserId()
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			return int.Parse(userId);
		}
		// A function to create a franchise
		public CreateFranchiseResponse CreateFranchise(CreateFranchiseRequest request)
		{
			try
			{
				// Creating a Social Profile
				FranchiseSocialModel franchiseSocial = new FranchiseSocialModel();
				franchiseSocial.FranchiseEmail = request.FrachiseSocial.FranchiseEmail;

				franchiseSocial.FranchiseFacebook = request.FrachiseSocial.FranchiseFacebook != "" ? request.FrachiseSocial.FranchiseFacebook : null;

				franchiseSocial.FranchiseWebsite = request.FrachiseSocial.FranchiseWebsite == "" ? null : request.FrachiseSocial.FranchiseWebsite;

				franchiseSocial.FranchiseInstagram = request.FrachiseSocial.FranchiseInstagram != "" ? request.FrachiseSocial.FranchiseInstagram : null;

				franchiseSocial.FranchiseTwitter = request.FrachiseSocial.FranchiseTwitter != "" ? request.FrachiseSocial.FranchiseTwitter : null;

				_context.FranchiseSocialModel.Add(franchiseSocial);

				_context.SaveChanges();


                // If FranchiseCustomizedOption is true
				if(request.Franchise.FranchiseCustomizedOption)
				{
                    //int franchiseCustomizedId = CreateFranchiseCustomization()

                }


                FranchiseModel newFranchise = new FranchiseModel()
				{
					FranchiseName = request.Franchise.FranchiseName,
					FranchiseAbout = request.Franchise.FranchiseAbout,
					FranchiseIndustry = request.Franchise.FranchiseIndustry,
					FranchiseSegment = request.Franchise.FranchiseSegment,
					FranchiseInvestment = request.Franchise.FranchiseInvestment,
					FranchiseFee = request.Franchise.FranchiseFee,
					FranchiseSpace = request.Franchise.FranchiseSpace,
					FranchiseCurrentCount = request.Franchise.FranchiseCurrentCount,
					FranchisePreferredExpansionLocation = request.Franchise.FranchisePreferredExpansionLocation,
					FranchiseViewCount = request.Franchise.FranchiseViewCount,
					FranchiseSampleBoxOption = request.Franchise.FranchiseSampleBoxOption,
					FranchiseCustomizedOption = request.Franchise.FranchiseCustomizedOption,
					FranchiseCustomizedOptionId = 1,
					FranchiseSocialId = franchiseSocial.FranchiseSocialId,
					FranchiseOwnerId = 1 // GetCurrentID() need to be called

				};

				_context.FranchiseModel.Add(newFranchise);

				_context.SaveChanges();

				// NEED TO Gallery

				foreach (var gallery in request.FranchiseGalleryList)
				{
					FranchiseGalleryModel newGallery = new FranchiseGalleryModel()
					{
						FranchisePhotoUrl = gallery.FranchisePhotoUrl,
						franchiseId = newFranchise
					};

					_context.FranchiseGalleryModel.Add(newGallery);
				}

				_context.SaveChanges();

				// NEED TO Services
				foreach (var service in request.FranchiseServiceList)
				{
					FranchiseServiceModel newService = new FranchiseServiceModel()
					{
						FranchiseProvideServiceName = service.FranchiseProvideServiceName,
						franchiseId =  newFranchise
					};
					_context.FranchiseServiceModel.Add(newService);
				}

				_context.SaveChanges();


				// NEED TO Reviews
				// A external request to RapidApi to get and store the review.

				return new CreateFranchiseResponse
				{
					Response = "Successfully Registered Franchise"
				};

			}
			catch (Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
		}

		public GetFranchiseResponse GetAllFranchise()
		{

			try
			{
				List<FranchiseModel> franchiseListFromDB = _context.FranchiseModel.Include(f => f.franchiseGallery).Include(f => f.franchiseServices).ToList();

				List<GetAllFranchiseResponse> franchiseListFromProto = new List<GetAllFranchiseResponse>();

				franchiseListFromDB.ForEach(franchise =>
				{
                    GetAllFranchiseResponse franchiseResponse = new GetAllFranchiseResponse()
					{
						Franchise = _mapper.Map<Franchise.Franchise>(franchise),
						FranchiseSocial = TypeCastingSocial(_context.FranchiseSocialModel.FirstOrDefault(f => f.FranchiseSocialId == franchise.FranchiseSocialId))
					};
				franchiseResponse.FranchiseGalleryList.Add(TypeCastingGalleryList(franchise.franchiseGallery));
				franchiseListFromProto.Add(franchiseResponse);
				}
				);

				GetFranchiseResponse response = new GetFranchiseResponse();

				response.FranchiseLists.Add(franchiseListFromProto);

				return response;

			}
			catch (Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
	
		}

		public GetFranchisesRespsonse GetFranchiseById(GetFranchiseByIdRequest request)
		{
			try
			{
				var franchiseDetailFromDB = _context.FranchiseModel.Where(f => f.FranchiseId == request.FranchiseId).Include(f => f.franchiseGallery).Include(f => f.franchiseServices).
					Include(f => f.franchiseReviews).FirstOrDefault();

				GetFranchisesRespsonse getFranchisesRespsonse = new GetFranchisesRespsonse()
				{
					Franchise = TypeCastingFranchise(franchiseDetailFromDB),
					FranchiseSocial = TypeCastingSocial(_context.FranchiseSocialModel.FirstOrDefault(f => f.FranchiseSocialId == franchiseDetailFromDB.FranchiseSocialId))
				};

				getFranchisesRespsonse.FranchiseGalleryList.AddRange(TypeCastingGalleryList(franchiseDetailFromDB.franchiseGallery));
				getFranchisesRespsonse.FrachiseServiceList.AddRange(TypeCastingServiceList(franchiseDetailFromDB.franchiseServices));

				return getFranchisesRespsonse;



            }
			catch(Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
		}


		// Type Casting Functions
		private Franchise.Franchise TypeCastingFranchise(FranchiseModel model)
		{
			Franchise.Franchise franchiseFromProto = new Franchise.Franchise()
			{
				FranchiseName = model.FranchiseName,
				FranchiseAbout = model.FranchiseAbout,
				FranchiseCurrentCount = model.FranchiseCurrentCount,
				FranchiseCustomizedOption = model.FranchiseCustomizedOption,
				FranchiseFee = model.FranchiseFee,
				FranchiseId = model.FranchiseId,
				FranchiseIndustry = model.FranchiseIndustry,
				FranchiseInvestment = model.FranchiseInvestment,
				FranchiseOwnerId = model.FranchiseOwnerId,
				FranchisePreferredExpansionLocation = model.FranchisePreferredExpansionLocation,
				FranchiseSampleBoxOption = model.FranchiseSampleBoxOption,
				FranchiseSegment = model.FranchiseSegment,
				FranchiseSpace = model.FranchiseSpace,
				FranchiseViewCount = model.FranchiseViewCount
			};

			return franchiseFromProto;
		}

		private FranchiseSocialModelResponse TypeCastingSocial(FranchiseSocialModel franchiseSocial)
		{
			FranchiseSocialModelResponse franchiseSocialModelResponseFromProto = new FranchiseSocialModelResponse()
			{
				FranchiseEmail = franchiseSocial.FranchiseEmail,
				FranchiseFacebook = franchiseSocial.FranchiseFacebook,
				FranchiseInstagram = franchiseSocial.FranchiseInstagram,
				FranchiseSocialId = franchiseSocial.FranchiseSocialId,
				FranchiseTwitter = franchiseSocial.FranchiseTwitter,
				FranchiseWebsite = franchiseSocial.FranchiseWebsite,
			};

			return franchiseSocialModelResponseFromProto;
		}

		private List<FranchiseGalleryResponse> TypeCastingGalleryList(ICollection<FranchiseGalleryModel> franchiseGallery)
		{
			List<FranchiseGalleryResponse> franchiseGalleryresponseFromProto = new List<FranchiseGalleryResponse>();
            foreach (var gallery in franchiseGallery)
            {
				franchiseGalleryresponseFromProto.Add(new FranchiseGalleryResponse()
				{
					FranchiseGalleryId = gallery.FranchiseGalleryId,
					FranchiseId = gallery.FranchiseId,
					FranchisePhotoUrl = gallery.FranchisePhotoUrl,
					
				});
            }
			return franchiseGalleryresponseFromProto;
        }
		private List<FranchiseServiceModelResponse> TypeCastingServiceList(ICollection<FranchiseServiceModel> franchiseServiceModels)
		{
            List<FranchiseServiceModelResponse> franchiseServiceresponseFromProto = new List<FranchiseServiceModelResponse>();
            foreach (var service in franchiseServiceModels)
            {
                franchiseServiceresponseFromProto.Add(new FranchiseServiceModelResponse()
                {
					FranchiseId = service.FranchiseId,
					FranchiseProvideServiceName = service.FranchiseProvideServiceName,
					FranchiseProvideServiceDescription = service.FranchiseProvideServiceDescription,
					FranchiseServiceId = service.FranchiseServiceId

                });
            }
            return franchiseServiceresponseFromProto;
        }

		
		// Creating a Franchise Customisation
		private int CreateFranchiseCustomization(List<FranchiseSelectedServiceRequest> franchiseSelectedServiceRequests)
		{
			FranchiseCustomizedOptionModel franchiseCustomizedOptionModel = new FranchiseCustomizedOptionModel();

			franchiseCustomizedOptionModel.FranchiseCustomizedOptionName = "test"; // TODO : NEED TO Generate Name from Franchise Name

			_context.FranchiseCustomizedOptionModel.Add(franchiseCustomizedOptionModel);
			_context.SaveChanges();

            foreach (var selectedServive in franchiseSelectedServiceRequests)
            {
				FranchiseSelectedService franchiseSelectedService = new FranchiseSelectedService()
				{
					FranchiseCustomizedOptionId = franchiseCustomizedOptionModel.FranchiseCustomizedOptionId,
					FranchiseProvideServiceId = selectedServive.FranchiseProvideServiceId,
				};
				_context.FranchiseSelectedServiceModel.Add(franchiseSelectedService);
            }

			_context.SaveChanges();


            return franchiseCustomizedOptionModel.FranchiseCustomizedOptionId;
		}
		
	}
}
