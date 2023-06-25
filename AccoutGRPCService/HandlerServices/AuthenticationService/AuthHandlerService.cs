using AccoutGRPCService.Data;
using AccoutGRPCService.Models;
using AccoutGRPCService.Protos;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AccoutGRPCService.HandlerServices.AuthenticationService
{
    public class AuthHandlerService : IAuthHandlerService
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FranchiseConnectContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHandlerService(FranchiseConnectContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        // To Get the Current User ID
        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }

        // Check User Already Exists
        private bool UserAlreadyExist(string userEmail)
        {
            bool userAlreadyExistResponse = (bool)_context.User?.Any(user => user.UserEmail == userEmail);
            return userAlreadyExistResponse;
        }

        // JWT Token Creation
        private string CreateToken(string userId, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        // Password Hash Generate
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Password Verification
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        // User Creation
        public UserCreationResponse UserCreation(UserCreationRequest userCreationRequest)
        {
            try
            {

                if (UserAlreadyExist(userCreationRequest.UserRequest.UserEmail))
                {
                    throw new RpcException(new Status(StatusCode.AlreadyExists, "User Already Exists"));
                }
                CreatePasswordHash(userCreationRequest.UserRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                UserModel newUser = new UserModel();

                newUser.UserEmail = userCreationRequest.UserRequest.UserEmail;
                newUser.UserPhoneNumber = userCreationRequest.UserRequest.UserPhoneNumber;
                newUser.UserName = userCreationRequest.UserRequest.UserName;
                newUser.UserRole = userCreationRequest.UserRequest.UserRole;
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;

                _context.User.Add(newUser);
                _context.SaveChanges();

                return new UserCreationResponse { Response = "Successfully Registered" };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }

        // User Sign In
        public AuthenticationResponse AuthenticateUser(AuthenticationRequest authenticationRequest)
        {
            try
            {
                if (!UserAlreadyExist(authenticationRequest.UserRequest.Email))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, $"{authenticationRequest.UserRequest.Email} is invalid."));
                    //return new BadRequestObjectResult($"{authenticationRequest.UserRequest.Email} is invalid.");
                }

                var user = _context.User.FirstOrDefault(user => user.UserEmail == authenticationRequest.UserRequest.Email);

                if (!VerifyPasswordHash(authenticationRequest.UserRequest.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid Password"));
                }

                string jwtToken = "";


                jwtToken = CreateToken(user.UserId.ToString(), user.UserRole);

                return new AuthenticationResponse
                {
                    JwtToken = jwtToken,
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }

        // A function to Change Password 
        public ChangePasswordResponse ChangePassword(ChangePasswordResquest changePasswordRequest)
        {
            try
            {
                var currentUserId = GetCurrentUserId();

                //string currentRole = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                var currentUser = _context.User.Where(user => user.UserId == currentUserId).FirstOrDefault();
                if (!VerifyPasswordHash(changePasswordRequest.OldPassword, currentUser.PasswordHash, currentUser.PasswordSalt))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Password"));
                }

                CreatePasswordHash(changePasswordRequest.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                currentUser.PasswordHash = passwordHash;
                currentUser.PasswordSalt = passwordSalt;

                _context.SaveChanges();



                return new ChangePasswordResponse { Response = "Successfully Password Changed" };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }

    }
}
