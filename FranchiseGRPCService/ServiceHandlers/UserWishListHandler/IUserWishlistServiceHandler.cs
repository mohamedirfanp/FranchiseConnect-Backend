using FranchiseGRPCService.Protos;

namespace FranchiseGRPCService.ServiceHandlers.UserWishListHandler
{
    public interface IUserWishlistServiceHandler
    {
        // A function to add a new wishlist
        UserWishListServiceResponse AddWishlist(AddUserWishListRequest addUserWishListRequest);

        // A function to get all user wishlist
        GetUserWishListResponse GetAllWishlist(GetUserWishListRequest getUserWishListRequest);

        // A function to remove user wishlist
        UserWishListServiceResponse RemoveWishlist(RemoveUserWishListRequest removeUserWishListRequest);
    }
}
