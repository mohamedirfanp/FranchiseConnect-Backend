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
        [Authorize]
        public override Task<GetProfileResponse> GetProfile(Empty request, ServerCallContext context)
        {
            var response = _accountService.GetProfile();

            return Task.FromResult(response);
        }

        [Authorize]
        // A function to update User Profile
        public override Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            return base.UpdateUser(request, context);
        }

    }
}
