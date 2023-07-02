using AccoutGRPCService.Protos;
using ApiGateway.GRPCMicroserviceClients;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IGRPCClients _grpcClients;

        public AccountService(IGRPCClients grpcClients)
        {
            _grpcClients = grpcClients;
        }

        public IActionResult CreateUser(UserSignUpDto user)
        {
            try
            {

                var response = _grpcClients.AuthClient.SignUpUser(new UserCreationRequest
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
           
                var response = _grpcClients.AuthClient.SignInUser(new AuthenticationRequest { UserRequest = user });

                return new OkObjectResult(response);

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
             
                var response = _grpcClients.AccountClient.GetProfile(new Google.Protobuf.WellKnownTypes.Empty());

                return response;
            }
            catch(RpcException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult UpdateProfile(UpdateUserRequest updateUserRequest)
        {
            try
            {

                var response = _grpcClients.AccountClient.UpdateUser(updateUserRequest);

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IActionResult ChangePassword(ChangePasswordResquest changePasswordRequest)
        {

            try
            {
              
                var response = _grpcClients.AuthClient.ChangePassword(changePasswordRequest);

                return new OkObjectResult(response);

            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
