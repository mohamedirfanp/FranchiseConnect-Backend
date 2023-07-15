
using AccoutGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace AccoutGRPCService.HandlerServices.AccountServices
{
    public interface IAccountHandlerService
    {
        // To Get User Profile
        GetProfileResponse GetProfile(GetProfileRequest profileRequest);


        // To Update Profile for User

        UpdateUserResponse UpdateProfileForUser(UpdateUserRequest user);

        // To get the user and franchisor count 

        GetAdminDetailResponse GetAdminProfile();

    }
}
