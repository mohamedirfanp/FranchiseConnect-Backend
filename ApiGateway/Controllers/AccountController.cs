using AccoutGRPCService.Protos;
using ApiGateway.Dto_Models;
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
	public class AccountController : ControllerBase
	{
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
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
		[HttpPost("franchisee/register")]
		public IActionResult UserRegister([FromBody] UserSignUpDto userRequest)
		{
			userRequest.UserRole = "Franchisee";
			
            var response = _accountService.CreateUser(userRequest);
            if (response is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            return Ok(response);

		}
        [HttpPost("franchisor/register")]
        public IActionResult FranchisorRegister([FromBody] UserSignUpDto userRequest)
        {
			userRequest.UserRole = "Franchisor";
            var response = _accountService.CreateUser(userRequest);
            if (response is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            return Ok(response);

        }

        // Post User Login
        [HttpPost("franchisee/login")]
		public IActionResult UserLogin([FromBody] UserSignInDto userSignInDto)
		{
			
			var response = _accountService.UserLogin(userSignInDto);
			if(response is BadRequestObjectResult badRequest)
			{
				return badRequest;
			}

			return Ok(response);
			
		}

        // Post User Login
        [HttpPost("franchisor/login")]
        public IActionResult FranchisorLogin([FromBody] UserSignInDto userSignInDto)
        {

            var response = _accountService.FranchisorLogin(userSignInDto);
            if (response is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }

            return Ok(response);

        }

        // Put to update user profile
        [HttpPut("update"), Authorize]
		public IActionResult UpdateProfile([FromBody] UpdataProfileDto updateUserRequest)
		{
			var response = _accountService.UpdateProfile(updateUserRequest);
            if (response is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            return Ok(response);
        }

		// Put to change password
		[HttpPut("change-password"), Authorize]
		public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordRequest)
		{
            var response = _accountService.ChangePassword(changePasswordRequest);
            if (response is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            return Ok(response);
		}


        // Profile Count
        [HttpGet("admin/profile-count"), Authorize(Roles = "Admin")]
        public GetAdminDetailResponse GetProfileCount()
        {
            var response = _accountService.GetProfileCountForAdmin();

            return response;

        }


	}
}
