using AccoutGRPCService.HandlerServices.AuthenticationService;
using AccoutGRPCService.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace AccoutGRPCService.Services
{
    public class AuthHandlerService : AuthenticationHandler.AuthenticationHandlerBase
    {
        private readonly IAuthService _authService;

        public AuthHandlerService(IAuthService authService)
        {
            _authService = authService;
        }

        public override Task<UserCreationResponse> SignUpUser(UserCreationRequest request, ServerCallContext context)
        {
            var response = _authService.UserCreation(request);
            if (response is BadRequestObjectResult badRequest)
            {
                if (badRequest.Equals("User already Exist"))
                    throw new RpcException(new Status(StatusCode.AlreadyExists, badRequest.Value.ToString()));
                else
                    throw new RpcException(new Status(StatusCode.Unknown, badRequest.Value.ToString()));
            }

            return Task.FromResult(new UserCreationResponse
            {
                Response = response.ToString(),
            });
        }

        public override Task<AuthenticationResponse> SignInUser(AuthenticationRequest request, ServerCallContext context)
        {
            var response = _authService.AuthenticateUser(request);
            /*if (response is BadRequestObjectResult badRequest)
            {
                if(badRequest.Equals("User already Exist"))
                    throw new RpcException(new Status(StatusCode.AlreadyExists, badRequest.Value.ToString()));
                else
                    throw new RpcException(new Status(StatusCode.Unknown, badRequest.Value.ToString()));
            }*/

            return Task.FromResult(response);
        }
    }
}
