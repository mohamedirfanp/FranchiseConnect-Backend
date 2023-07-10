using AccoutGRPCService.HandlerServices.AccountServices;
using AccoutGRPCService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace AccoutGRPCService.Services
{
    public class AccountService : AccountHandler.AccountHandlerBase
    {
        private IAccountHandlerService _accountService;

        public AccountService(IAccountHandlerService accountService)
        {
            _accountService = accountService; 
        }

        // A function to get User Profile
        
        public override Task<GetProfileResponse> GetProfile(GetProfileRequest profileRequest, ServerCallContext context)
        {
            var response = _accountService.GetProfile(profileRequest);

            return Task.FromResult(response);
        }

        // A function to update User Profile
        public override Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var response = _accountService.UpdateProfileForUser(request);

            return Task.FromResult(response);
        }

    }
}
