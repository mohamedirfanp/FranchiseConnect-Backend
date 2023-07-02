using AccoutGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.AccountService
{
    public interface IAccountService
    {
        // A User Sign Up function
        IActionResult CreateUser(UserSignUpDto user);

        // A User Sign In function
        IActionResult UserLogin(UserSignInDto user);

        // A function to get user profile
        GetProfileResponse GetProfile();


        // A function to Update Profile
        IActionResult UpdateProfile(UpdateUserRequest updateUserRequest);

        // A function to Update Password
        IActionResult ChangePassword(ChangePasswordResquest changePasswordRequest);

       

    }
}
