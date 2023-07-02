using FranchiseGRPCService.Data;
using FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers;
using FranchiseGRPCService.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FranchiseGRPCService.ServiceHandlers.FranchiseGalleryHandlers;
using FranchiseGRPCService.ServiceHandlers.FranchiseProvideServiceHandlers;
using FranchiseGRPCService.ServiceHandlers.FranchiseRequestHandlers;
using FranchiseGRPCService.ServiceHandlers.UserWishListHandler;

namespace FranchiseGRPCService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get the IConfiguration instance
            IConfiguration configuration = builder.Configuration;

            builder.Services.AddJwtAuthentication(configuration);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            //var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

            var connectionString = "Server=localhost;Database=FranchiseConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false;";

            builder.Services.AddDbContext<FranchiseConnectContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();

            // Add AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IFranchiseServiceHandler, FranchiseServiceHandler>();
            builder.Services.AddScoped<IFranchiseGalleryHandler,FranchiseGalleryHandler>();
            builder.Services.AddScoped<IFranchiseProvideServiceHandler, FranchiseProvideServiceHandler>();
            builder.Services.AddScoped<IFranchiseRequestHandler, FranchiseRequestHandler>();
            builder.Services.AddScoped<IUserWishlistServiceHandler, UserWishlistServiceHandler>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();
            app.MapGrpcService<Services.franchiseService>();
            app.MapGrpcService<Services.FranchiseGalleryServices>();
            app.MapGrpcService<Services.FranchiseProvideService>();
            app.MapGrpcService<Services.FranchiseRequest>();
            app.MapGrpcService<Services.UserWishlistService>();

            app.UseAuthentication();
            app.UseAuthorization();
          
            // DEV
            IWebHostEnvironment env = app.Environment;
            if (env.IsDevelopment())
            {
                app.MapGrpcReflectionService();
            }


            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}