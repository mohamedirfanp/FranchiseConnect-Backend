using AccoutGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace AccoutGRPCService.HandlerServices.AuthenticationService
{
    public interface IAuthService
    {
        // User Sign Up
        UserCreationResponse UserCreation(UserCreationRequest userCreationRequest);

        // User Sign In
        AuthenticationResponse AuthenticateUser(AuthenticationRequest authenticationRequest);
    }
}
