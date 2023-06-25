using AccoutGRPCService.Data;
using AccoutGRPCService.HandlerServices.AuthenticationService;
using AccoutGRPCService.HandlerServices.AccountServices;
using AccoutGRPCService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AccoutGRPCService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dbHost = "localhost";
            var dbName = "FranchiseConnectDB";
            var dbPassword = "Password";

            //var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

            var connectionString = "Server=localhost;Database=FranchiseConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false;";

            builder.Services.AddDbContext<FranchiseConnectContext>(options =>
    options.UseSqlServer(connectionString));

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682


            // Add JWT Middleware
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IAuthHandlerService, AuthHandlerService>();
            builder.Services.AddScoped<IAccountHandlerService, AccountHandlerService>();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<Services.AuthService>();
            app.MapGrpcService<Services.AccountService>();

            app.UseAuthentication();
            app.UseAuthorization();

            // DEV
            IWebHostEnvironment env = app.Environment;
            if(env.IsDevelopment())
            { 
                app.MapGrpcReflectionService();
            }

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}