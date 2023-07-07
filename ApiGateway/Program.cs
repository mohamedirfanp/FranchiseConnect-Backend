
using ApiGateway.Services.AccountService;
using ApiGateway.Services.FranchiseService;
using ApiGateway.GRPCMicroserviceClients;
using ApiGateway.Services.ChatService;

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
                });
            });

            // Add Client for GRPC Services
            builder.Services.AddSingleton<IGRPCClients, GRPCClients>();

            // Add services to the container.
            builder.Services.AddHttpContextAccessor(); 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<IFranchiseService, FranchiseServiceApi>();
            builder.Services.AddScoped<IChatService, ChatService>();


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
            app.UseCors("AllowOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}