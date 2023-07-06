using AccoutGRPCService.HandlerServices.AuthenticationService;
using AccoutGRPCService.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccoutGRPCService.Services
{
    public class AuthService : AuthenticationHandler.AuthenticationHandlerBase
    {
        private readonly IAuthHandlerService _authService;

        public AuthService(IAuthHandlerService authService)
        {
            _authService = authService;
        }

        public override Task<UserCreationResponse> SignUpUser(UserCreationRequest request, ServerCallContext context)
        {
            var response = _authService.UserCreation(request);


            return Task.FromResult(new UserCreationResponse
            {
                Response = response.ToString(),
            });
        }

        public override Task<AuthenticationResponse> SignInUser(AuthenticationRequest request, ServerCallContext context)
        {
            var response = _authService.AuthenticateUser(request);

            return Task.FromResult(response);
        }

        public override Task<AuthenticationResponse> SignInFranchisor(AuthenticationRequest request, ServerCallContext context)
        {
            var response = _authService.AuthenticateFranchisor(request);

            return Task.FromResult(response);
        }

        [Authorize]
        public override Task<ChangePasswordResponse> ChangePassword(ChangePasswordResquest request, ServerCallContext context)
        {
            var response = _authService.ChangePassword(request);

            return Task.FromResult(response);
        }



    }
}
