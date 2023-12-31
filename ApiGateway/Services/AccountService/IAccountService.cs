﻿using AccoutGRPCService.Protos;
using ApiGateway.Dto_Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.AccountService
{
    public interface IAccountService
    {
        // A User Sign Up function
        IActionResult CreateUser(UserSignUpDto user);

        // A User Sign In function
        IActionResult UserLogin(UserSignInDto user);

        // A franchisor in function
        IActionResult FranchisorLogin(UserSignInDto user);

        // A function to get user profile
        GetProfileResponse GetProfile();


        // A function to Update Profile
        IActionResult UpdateProfile(UpdataProfileDto updateUserRequest);

        // A function to Update Password
        IActionResult ChangePassword(ChangePasswordDto changePasswordRequest);

        // A function to Get Profile Count

        GetAdminDetailResponse GetProfileCountForAdmin();


    }
}
