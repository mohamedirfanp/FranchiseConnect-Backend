
using AccoutGRPCService.Data;
using AccoutGRPCService.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccoutGRPCService.HandlerServices.AccountServices
{
	public class AccountHandlerService : IAccountHandlerService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly FranchiseConnectContext _context;


		public AccountHandlerService(FranchiseConnectContext context, IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_context = context;
		}

		public GetProfileResponse GetProfile()
		{
			try
			{
				var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

				//dynamic currentProfile = new System.Dynamic.ExpandoObject();

				//currentProfile.ProvidedService = new List<ProvidedServiceModel>();


				var currentUser = _context.User.Where(user => user.UserId == currentUserId).Select(user => new
				{
					user.UserName,
					user.UserEmail,
					user.UserPhoneNumber
				}).FirstOrDefault();

				//currentProfile.Profile = currentUser;

				UserDto currentProfile = new UserDto
				{
					Name = currentUser.UserName,
					Email = currentUser.UserEmail,
					PhoneNumber = currentUser.UserPhoneNumber
				};

				return new GetProfileResponse { Response = currentProfile };

			}
			catch (Exception ex)
			{
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));

            }
		}

		public UpdateUserResponse UpdateProfileForUser(UpdateUserRequest user)
		{
			try
			{
				var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
				var currentUser = _context.User.Where(user => user.UserId == currentUserId).FirstOrDefault();

				currentUser.UserPhoneNumber = user.Request.PhoneNumber;
				currentUser.UserName = user.Request.Name;

				_context.SaveChanges();

				return new UpdateUserResponse { Response = "Successfully Updated Profile" };

			}
			catch (Exception ex)
			{
				throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
			}
		}

	}
}
