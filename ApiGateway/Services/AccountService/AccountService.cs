using AccoutGRPCService.Protos;
using ApiGateway.Dto_Models;
using ApiGateway.GRPCMicroserviceClients;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiGateway.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IGRPCClients _grpcClients;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IGRPCClients grpcClients, IHttpContextAccessor httpContextAccessor)
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

        public IActionResult CreateUser(UserSignUpDto user)
        {
            try
            {

                UserCreationResponse response = _grpcClients.AuthClient.SignUpUser(new UserCreationRequest
                {
                    UserRequest = user
                });

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                
                return new BadRequestObjectResult(ex.Message);
            }
        }
        public IActionResult UserLogin(UserSignInDto user)
        {
         
            try
            {
                AuthenticationResponse response = _grpcClients.AuthClient.SignInUser(new AuthenticationRequest { UserRequest = user });

                return new OkObjectResult(response.JwtToken);

            }
            catch (RpcException ex)
            {

                return new BadRequestObjectResult(ex.Message);
            }
        }

        public IActionResult FranchisorLogin(UserSignInDto user)
        {
            try
            {
                AuthenticationResponse response = _grpcClients.AuthClient.SignInFranchisor(new AuthenticationRequest { UserRequest = user });

                return new OkObjectResult(response.JwtToken);

            }
            catch (RpcException ex)
            {

                return new BadRequestObjectResult(ex.Message);
            }
        }

        public GetProfileResponse GetProfile()
        {
            try
            {

                var response = _grpcClients.AccountClient.GetProfile(new GetProfileRequest
                {
                    UserId = GetCurrentUserId()
                });

                return response;
            }
            catch(RpcException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult UpdateProfile(UpdataProfileDto updateUserRequest)
        {
            try
            {

                var response = _grpcClients.AccountClient.UpdateUser(new UpdateUserRequest
                {
                    UserId = GetCurrentUserId(),
                    Request = new UserDto
                    {
                        Name = updateUserRequest.Name,
                        Email = updateUserRequest.Email,
                        ProfilePhotoUrl = updateUserRequest.ProfilePhotoUrl,
                        PhoneNumber = updateUserRequest.PhoneNumber,
                    }
                });

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IActionResult ChangePassword(ChangePasswordDto changePasswordRequest)
        {

            try
            {
              
                var response = _grpcClients.AuthClient.ChangePassword(new ChangePasswordResquest
                {
                    UserId = GetCurrentUserId(),
                    OldPassword = changePasswordRequest.oldPassword,
                    NewPassword = changePasswordRequest.newPassword

                });

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

       
    }
}
