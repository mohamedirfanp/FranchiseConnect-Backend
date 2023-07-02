using AutoMapper;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.Models;
using FranchiseGRPCService.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FranchiseGRPCService.ServiceHandlers.UserWishListHandler
{
    public class UserWishlistServiceHandler : IUserWishlistServiceHandler
    {
        private readonly FranchiseConnectContext _context;
        private readonly IMapper _mapper;

        public UserWishlistServiceHandler(FranchiseConnectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserWishListServiceResponse AddWishlist(AddUserWishListRequest addUserWishListRequest)
        {
            try
            {
                UserWishlistModel userWishlistModel = new UserWishlistModel();
                userWishlistModel.FranchiseId = addUserWishListRequest.FranchiseId;
                userWishlistModel.UserId = addUserWishListRequest.UserId;

                _context.UserWishlistModel.Add(userWishlistModel);
                _context.SaveChanges();

                return new UserWishListServiceResponse
                {
                    Response = "Successfully Added"
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public GetUserWishListResponse GetAllWishlist(GetUserWishListRequest getUserWishListRequest)
        {
            try
            {
                var response = new GetUserWishListResponse();

                var userWishList = _context.UserWishlistModel
                    .Where(u => u.UserId == getUserWishListRequest.UserId)
                    .Include(u => u.franchiseId)
                    .ToList();

                foreach (var userWishlistModel in userWishList)
                {
                    response.UserWishlistResponse.Add(new UserWishList
                    {
                        Franchise = _mapper.Map<Franchise.Franchise>(userWishlistModel.franchiseId),
                        UserId = userWishlistModel.UserId
                    });
                }

                return response;

            }
            catch(Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public UserWishListServiceResponse RemoveWishlist(RemoveUserWishListRequest removeUserWishListRequest)
        {
            try
            {
                UserWishlistModel userWishlistModel = _context.UserWishlistModel.Find(removeUserWishListRequest.UserWishlistId);


                _context.UserWishlistModel.Remove(userWishlistModel);
                

                return new UserWishListServiceResponse
                {
                    Response = "Successfully Removed"
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
