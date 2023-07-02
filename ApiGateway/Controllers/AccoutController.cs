using AccoutGRPCService.Protos;
using ApiGateway.Services.AccountService;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccoutController : ControllerBase
	{
        private readonly IAccountService _accountService;

        public AccoutController(IAccountService accountService)
		{
			_accountService = accountService;
		}


		// GET: api/<AccoutController>
		[HttpGet, Authorize]
		public GetProfileResponse Get()
		{
			var response = _accountService.GetProfile();

			return response;
		}

		// POST api/<AccoutController>
		[HttpPost]
		public IActionResult Post([FromBody] UserSignUpDto userRequest)
		{
			var response = _accountService.CreateUser(userRequest);

			return Ok(response);

		}

		// Post User Login
		[HttpPost("user/login")]
		public IActionResult UserLogin([FromBody] UserSignInDto userSignInDto)
		{
			var response = _accountService.UserLogin(userSignInDto);

			return Ok(response);
			
		}

		// Put to update user profile
		[HttpPut, Authorize]
		public IActionResult UpdateProfile([FromBody] UpdateUserRequest updateUserRequest)
		{
			var response = _accountService.UpdateProfile(updateUserRequest);

			return Ok(response);
		}

		// Put to change password
		[HttpPut, Authorize]
		public IActionResult ChangePassword([FromBody] ChangePasswordResquest changePasswordRequest)
		{
			var response = _accountService.ChangePassword(changePasswordRequest);

			return Ok(response);
		}

	}
}
