using AccoutGRPCService.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccoutController : ControllerBase
	{
		// GET: api/<AccoutController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<AccoutController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<AccoutController>
		[HttpPost]
		public void Post([FromBody] UserSignUpDto userRequest)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7212");
			var client  = new AuthenticationHandler.AuthenticationHandlerClient(channel);

			var response = client.SignUpUser(new UserCreationRequest
			{
				UserRequest = userRequest
			});

			Console.WriteLine(response);
			Console.ReadLine();
			channel.ShutdownAsync().Wait();
		}

		// PUT api/<AccoutController>/5
		[HttpPost("user/login")]
		public void UserLogin([FromBody] UserSignInDto userSignInDto)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:7212");

			try
			{
				var client  = new AuthenticationHandler.AuthenticationHandlerClient(channel);
				var response = client.SignInUser(new AuthenticationRequest { UserRequest = userSignInDto });    
				Console.WriteLine(response);

			}
			catch(RpcException ex)
			{
				Console.WriteLine(ex.Message, ex.StatusCode);
			}
			channel.ShutdownAsync().Wait();
		}

		// DELETE api/<AccoutController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
