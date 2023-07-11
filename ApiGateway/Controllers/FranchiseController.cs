using FranchiseGRPCService.Protos;
using Grpc.Net.Client;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Franchise;
using ApiGateway.Dto_Models;
using ApiGateway.Services.FranchiseService;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FranchiseController : ControllerBase
	{
        private readonly IFranchiseService _franchiseService;

        public FranchiseController(IFranchiseService franchiseService) 
		{
			_franchiseService = franchiseService;
		}

		// GET: api/<FranchiseController>
		[HttpGet, Authorize]
		public GetFranchiseResponse Get()
		{
			var response = _franchiseService.GetAllFranchise();

			return response;

		}

		// GET api/<FranchiseController>/5
		[HttpGet("{id}"), Authorize]
		public GetFranchisesRespsonse Get(int id)
		{
			var response = _franchiseService.GetFranchiseById(id);

			return response;
		}


		// GET on OwnerId
		[HttpGet("franchise-account"), Authorize]
		public GetFranchisesRespsonse GetByOwnerId()
		{
            var response = _franchiseService.GetFranchiseByOwnerId();

            return response;
        }

		// POST api/<FranchiseController>
		[HttpPost, Authorize(Roles="Franchisor")]
		public CreateFranchiseResponse Post([FromBody] CreateFranchiseDto request)
		{
			var response = _franchiseService.CreateFranchise(request);

			return response;

		}

		// Get Franchise Exist Status
		[HttpGet("franchise-exist"), Authorize(Roles="Franchisor")]
		public FranchiseExistResponse GetExist()
		{
			var response = _franchiseService.GetFranchiseExist();

			return response;
		}

		// Update Franchise Details
		[HttpPut("update"), Authorize(Roles ="Franchisor")]
		public UpdateFranchiseDetailResponse UpdateFranchise(UpdateFranchiseDetailRequest request)
		{
			var response = _franchiseService.UpdateFranchise(request);
			return response;
		}

		// Put - Increment Franchise View Count
		[HttpPut("update-viewcount"), Authorize]
		public IncreseViewCountResponse UpdateViewCount([FromBody] IncreseViewCountRequest request)
		{
            var response = _franchiseService.IncrementViewCount(request);

            return response;
        }


		/// Franchise Gallery Endpoints
		// PUT Franchise - api/<FranchiseController>/gallery
		[HttpPut("gallery/upload"), Authorize]
		public IActionResult uploadGalleryRequest([FromBody] FranchiseGalleryUploadRequest request)
		{
			var response = _franchiseService.UploadGallery(request);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		[HttpDelete("gallery/delete/{galleryId}"), Authorize]
		public IActionResult deleteGalleryRequest(int galleryId)
		{
            var response = _franchiseService.DeleteGallery(new FranchiseGalleryDeleteRequest
			{
				FranchiseGalleryId = galleryId
			});

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);

        }


		/* Franchise Provide Service Endpoints*/

		// Create a new Provide Service in the Franchise 
		[HttpPut("service/create"), Authorize]
		public IActionResult CreateProvideService([FromBody] CreateFranchiseServiceRequest request)
		{
            var response = _franchiseService.CreateProvideService(request);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		// Delete a existing Provided Service in the Franchise
		[HttpDelete("service/delete/{franchise_provide_service_id}"), Authorize]
		public IActionResult DeleteProvideService(int franchise_provide_service_id)
		{
            var response = _franchiseService.DeleteProvidedService(franchise_provide_service_id);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		/* Franchise Request Service Endpoints */

		// Create a User franchise request
		[HttpPost("request"), Authorize(Roles = "Franchisee")]
		public IActionResult CreateUserRequest([FromBody] CreateRequestDto createFranchiseUserRequest)
		{

			var response = _franchiseService.CreateUserRequest(createFranchiseUserRequest);


			if (response is BadRequestObjectResult badRequest)
			{
				return BadRequest(badRequest.Value);
			}

			return Ok(response);
		}

		// Get all franchise Pending Request for franchisor
		[HttpGet("pending-request"), Authorize(Roles = "Franchisor")]

		public FranchiseRequestResponseList GetAllFranchiseUserRequest()
		{
			var response = _franchiseService.GetAllFranchiseRequest();

			return response;
		}

		// Get all franchise request for franchisor
		[HttpGet("request"), Authorize]
		public FranchiseRequestResponseList GetAllFranchiseRequest()
		{
            var response = _franchiseService.GetAllFranchiseRequests();

            return response;
        }


        // To Update a franchise Request Status
        [HttpPut("request"), Authorize(Roles = "Franchisor")]
		public IActionResult UpdateUserRequestStatus([FromBody] UpdateStatusRequest statusRequest)
		{
            var response = _franchiseService.UpdateRequestStatus(statusRequest);


            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }


		/* User Wishlist Service Endpoints */

		// Add a User wishlist
		[HttpPut("user-wishlist"), Authorize(Roles ="Franchisee")]
		public IActionResult AddWishlist([FromBody] CommonRequest franchiseId)
		{
            var response = _franchiseService.AddUserWishlist(franchiseId);


            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		[HttpGet("user-wishlist"), Authorize(Roles = "Franchisee")]
		public GetUserWishListResponse GetUserWishlistList()
		{
			var response = _franchiseService.GetAllWishlist();

			return response;
		}

		[HttpDelete("user-wishlist/{wishlistId}"), Authorize(Roles = "Franchisee")]
		public IActionResult RemoveWishList(int wishlistId)
		{
            var response = _franchiseService.RemoveUserWishlist(new RemoveUserWishListRequest
			{
				UserWishlistId = wishlistId
			});


            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

    }
}
