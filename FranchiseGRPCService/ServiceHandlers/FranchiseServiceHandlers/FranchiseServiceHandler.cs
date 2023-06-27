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
		public FranchiseServiceHandler(FranchiseConnectContext context, IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_context = context;
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
				FranchiseSocialModel franchiseSocial = new FranchiseSocialModel();
				franchiseSocial.FranchiseEmail = request.FrachiseSocial.FranchiseEmail;

				franchiseSocial.FranchiseFacebook = request.FrachiseSocial.FranchiseFacebook != "" ? request.FrachiseSocial.FranchiseFacebook : null;

				franchiseSocial.FranchiseWebsite = request.FrachiseSocial.FranchiseWebsite == "" ? null : request.FrachiseSocial.FranchiseWebsite;

				franchiseSocial.FranchiseInstagram = request.FrachiseSocial.FranchiseInstagram != "" ? request.FrachiseSocial.FranchiseInstagram : null;

				franchiseSocial.FranchiseTwitter = request.FrachiseSocial.FranchiseTwitter != "" ? request.FrachiseSocial.FranchiseTwitter : null;

				_context.FranchiseSocialModel.Add(franchiseSocial);

				_context.SaveChanges();

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
					FranchiseOwnerId = 1

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

				_context.SaveChangesAsync();

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

				_context.SaveChangesAsync();


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

		public void GetAllFranchise()
		{

			try
			{
				List<FranchiseModel> franchiseList = _context.FranchiseModel.Include(f => f.franchiseGallery).Include(f => f.franchiseServices).ToList();
				foreach (var item in franchiseList)
				{
					Console.WriteLine("Item ", item);
				}

				return;
			}
			catch (Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
	
		}

		public GetFranchiseResponse GetFranchiseById(GetFranchiseByIdRequest request)
		{
			try
			{
				var franchiseDetail = _context.FranchiseModel.Where(f => f.FranchiseId == request.FranchiseId).FirstOrDefault();
				Console.WriteLine(franchiseDetail);

				throw new NotImplementedException();
			}
			catch(Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
		}
	}
}
