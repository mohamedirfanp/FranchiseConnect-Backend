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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FranchiseController>/5
        [HttpGet("{id}")]
        public void Get(int id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7290");

            try
            {
                var client = new FranchiseService.FranchiseServiceClient(channel);
                var response = client.GetFranchiseById(new GetFranchiseByIdRequest { FranchiseId = id });
                Console.WriteLine(response);

            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message, ex.StatusCode);
            }
            channel.ShutdownAsync().Wait();
        }

        // POST api/<FranchiseController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateFranchiseDto request)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7290");

            try
            {
                Console.WriteLine("Data" + request);
                Console.WriteLine("Franchise About "+ request.Franchise.FranchiseAbout);
                Console.WriteLine("Franchise Social webiste "+ request.FranchiseSocialRequest.FranchiseWebsite);

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
                        FranchiseProvideServiceName = service
                    });
         
                }


                var response = client.CreateFranchise(newFranchise);
                Console.WriteLine(response);


            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message, ex.StatusCode);
            }
            return Ok(request);
            //channel.ShutdownAsync().Wait();
        }

        // PUT api/<FranchiseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FranchiseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
