using FranchiseGRPCService.Protos;
using Grpc.Net.Client;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Franchise;
using ApiGateway.Dto_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FranchiseController : ControllerBase
	{
		// GET: api/<FranchiseController>
		[HttpGet]
		public GetFranchiseResponse Get()
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7290");
			try
			{
				var client = new FranchiseService.FranchiseServiceClient(channel);

				var response = client.GetAllFranchise(new Google.Protobuf.WellKnownTypes.Empty { });

				channel.ShutdownAsync().Wait();
				return response;

			}
			catch(RpcException ex)
			{
				throw new RpcException(new Status(Grpc.Core.StatusCode.Unimplemented, ex.Message)); 
			}

		}

		// GET api/<FranchiseController>/5
		[HttpGet("{id}")]
		public GetFranchisesRespsonse Get(int id)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7290");

			try
			{
				var client = new FranchiseService.FranchiseServiceClient(channel);
				var response = client.GetFranchiseById(new GetFranchiseByIdRequest { FranchiseId = id });
				
				channel.ShutdownAsync().Wait();

				return response;

			}
			catch (RpcException ex)
			{
				throw new RpcException(new Status(Grpc.Core.StatusCode.Unimplemented, ex.Message));
			}
		}

		// POST api/<FranchiseController>
		[HttpPost]
		public IActionResult Post([FromBody] CreateFranchiseDto request)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7290");

			try
			{

				var client = new FranchiseService.FranchiseServiceClient(channel);

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


				var response = client.CreateFranchise(newFranchise);
				return Ok(response);

			}
			catch (RpcException ex)
			{
				return BadRequest(ex.Message);
			}
			
			//channel.ShutdownAsync().Wait();
		}


		/// Franchise Gallery Endpoints
		// PUT Franchise - api/<FranchiseController>/gallery
		[HttpPut("gallery/upload")]
		public IActionResult uploadGalleryRequest([FromBody] FranchiseGalleryUploadRequest request)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7290");

			try
			{
				var client = new FranchiseGalleryService.FranchiseGalleryServiceClient(channel);

				var response = client.UploadFranchisePhoto(request);

				return Ok(response);

			}
			catch (RpcException ex)
			{
				return BadRequest(ex.Message);
            }
		}

		[HttpDelete("gallery/delete")]
		public IActionResult deleteGalleryRequest([FromBody] FranchiseGalleryDeleteRequest request)
		{

            try
            {
				var channel = GrpcChannel.ForAddress("https://localhost:7290");
                var client = new FranchiseGalleryService.FranchiseGalleryServiceClient(channel);

				var response = client.DeleteFranchisePhoto(request);

                return Ok(response);

            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Message);
            }
        }


		/* Franchise Provide Service Endpoints*/

		// Create a new Provide Service in the Franchise 
		[HttpPut("service/create")]
		public IActionResult CreateProvideService([FromBody] CreateFranchiseServiceRequest request)
		{
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7290");
				var client = new FranchiseServiceService.FranchiseServiceServiceClient(channel);
				
				var response = client.CreateFranchiseService(request);

                return Ok(response);

            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Message);
            }
        }

		// Delete a existing Provided Service in the Franchise
		[HttpDelete("service/delete/{franchise_provide_service_id}")]
		public IActionResult DeleteProvideService(int franchise_provide_service_id)
		{
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7290");
                var client = new FranchiseServiceService.FranchiseServiceServiceClient(channel);

                var response = client.DeleteFranchiseService(new DeleteFranchiseServiceRequest()
				{
					FranchiseProvideServiceId = franchise_provide_service_id
				});

                return Ok(response);

            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Message);
            }
        }


	}
}
