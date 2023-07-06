

using ChatGRPCService.Data;
using ChatGRPCService.HandlerServices.ConversationsServiceHandler;
using ChatGRPCService.HandlerServices.QueriesServiceHandler;
using ChatGRPCService.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatGRPCService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // DB Connection
            var connectionString = "Server=localhost;Database=FranchiseConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false;";

            builder.Services.AddDbContext<FranchiseConnectContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddScoped<IConversionServiceHandler, ConversationServiceHandler>();
            builder.Services.AddScoped<IQueryServiceHandler, QueryServiceHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();
            app.MapGrpcService<ConversationsService>();
            app.MapGrpcService<QueriesService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}