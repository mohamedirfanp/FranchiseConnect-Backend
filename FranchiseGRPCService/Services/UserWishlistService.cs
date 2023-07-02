using FranchiseGRPCService.Protos;
using FranchiseGRPCService.ServiceHandlers.UserWishListHandler;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class UserWishlistService : UserWishListService.UserWishListServiceBase
    {
        private readonly IUserWishlistServiceHandler _userWishlistServiceHandler;

        public UserWishlistService(IUserWishlistServiceHandler userWishlistServiceHandler) 
        {
            _userWishlistServiceHandler = userWishlistServiceHandler;
        }

        public override Task<UserWishListServiceResponse> AddUserWishList(AddUserWishListRequest request, ServerCallContext context)
        {
            var response = _userWishlistServiceHandler.AddWishlist(request);

            return Task.FromResult(response);

        }

        public override Task<GetUserWishListResponse> GetUserWishList(GetUserWishListRequest request, ServerCallContext context)
        {
            var response = _userWishlistServiceHandler.GetAllWishlist(request);

            return Task.FromResult(response);
        }

        public override Task<UserWishListServiceResponse> RemoveUserWishList(RemoveUserWishListRequest request, ServerCallContext context)
        {
            var response = _userWishlistServiceHandler.RemoveWishlist(request);

            return Task.FromResult(response);
        }
    }
}
