
using ApiGateway.Services.AccountService;
using ApiGateway.Services.FranchiseService;
using ApiGateway.GRPCMicroserviceClients;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            // Get the IConfiguration instance
            IConfiguration configuration = builder.Configuration; 

            builder.Services.AddJwtAuthentication(configuration);

            // Add Client for GRPC Services
            builder.Services.AddSingleton<IGRPCClients, GRPCClients>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<IFranchiseService, FranchiseServiceApi>();


            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}