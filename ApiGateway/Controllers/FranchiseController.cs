using FranchiseGRPCService.Protos;
using Grpc.Net.Client;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Franchise;
using ApiGateway.Dto_Models;
using ApiGateway.Services.FranchiseService;

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
		[HttpGet]
		public GetFranchiseResponse Get()
		{
			var response = _franchiseService.GetAllFranchise();

			return response;

		}

		// GET api/<FranchiseController>/5
		[HttpGet("{id}")]
		public GetFranchisesRespsonse Get(int id)
		{
			var response = _franchiseService.GetFranchiseById(id);

			return response;
		}

		// POST api/<FranchiseController>
		[HttpPost]
		public CreateFranchiseResponse Post([FromBody] CreateFranchiseDto request)
		{
			var response = _franchiseService.CreateFranchise(request);

			return response;

		}


		/// Franchise Gallery Endpoints
		// PUT Franchise - api/<FranchiseController>/gallery
		[HttpPut("gallery/upload")]
		public IActionResult uploadGalleryRequest([FromBody] FranchiseGalleryUploadRequest request)
		{
			var response = _franchiseService.UploadGallery(request);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		[HttpDelete("gallery/delete")]
		public IActionResult deleteGalleryRequest([FromBody] FranchiseGalleryDeleteRequest request)
		{
            var response = _franchiseService.DeleteGallery(request);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);

        }


		/* Franchise Provide Service Endpoints*/

		// Create a new Provide Service in the Franchise 
		[HttpPut("service/create")]
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
		[HttpDelete("service/delete/{franchise_provide_service_id}")]
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
		[HttpPost("request/create")]
		public IActionResult CreateUserRequest([FromBody] CreateFranchiseUserRequest createFranchiseUserRequest)
		{
			var response = _franchiseService.CreateUserRequest(createFranchiseUserRequest);


			if (response is BadRequestObjectResult badRequest)
			{
				return BadRequest(badRequest.Value);
			}

			return Ok(response);
		}

		// Get all franchise Request for franchisor
		[HttpGet("request/get")]
		public FranchiseRequestResponseList GetAllFranchiseUserRequest()
		{
			var response = _franchiseService.GetAllFranchiseRequest();

			return response;
		}

		// To Update a franchise Request Status
		[HttpPut("request/update")]
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
		[HttpPost("user-wishlist/add")]
		public IActionResult AddWishlist([FromBody] AddUserWishListRequest userWishListRequest)
		{
            var response = _franchiseService.AddUserWishlist(userWishListRequest);


            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

		[HttpGet("user-wishlist/get")]
		public GetUserWishListResponse GetUserWishlistList(GetUserWishListRequest wishListRequest)
		{
			var response = _franchiseService.GetAllWishlist(wishListRequest);

			return response;
		}

		[HttpDelete("user-wishlist/remove")]
		public IActionResult RemoveWishList(RemoveUserWishListRequest removeUserWishRequest)
		{
            var response = _franchiseService.RemoveUserWishlist(removeUserWishRequest);


            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

    }
}
