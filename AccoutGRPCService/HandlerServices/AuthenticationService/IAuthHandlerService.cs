using AccoutGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace AccoutGRPCService.HandlerServices.AuthenticationService
{
    public interface IAuthHandlerService
    {
        // User Sign Up
        UserCreationResponse UserCreation(UserCreationRequest userCreationRequest);

        // User Sign In
        AuthenticationResponse AuthenticateUser(AuthenticationRequest authenticationRequest);

        // To Change Password
        ChangePasswordResponse ChangePassword(ChangePasswordResquest changePasswordRequest);
    }
}
