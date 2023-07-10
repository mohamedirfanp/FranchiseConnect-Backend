using ApiGateway.Dto_Models;
using ApiGateway.GRPCMicroserviceClients;
using ChatGRPCService.Protos;
using ChatPackage;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiGateway.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IGRPCClients _grpcClients;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatService(IGRPCClients grpcClients, IHttpContextAccessor httpContextAccessor)
        {
            _grpcClients = grpcClients;
            _httpContextAccessor = httpContextAccessor;
        }

        // To Get the Current User ID
        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }


        public IActionResult CreateConversation(CreateConversationRequest conversationRequest)
        {
            try
            {
                var response = _grpcClients.ConversationServiceClient.CreateConversation(conversationRequest);

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public GetConversationsResponse GetConversations()
        {
            try
            {
                var response = _grpcClients.ConversationServiceClient.GetConversations(new CommonRequest
                {
                    RequestId = GetCurrentUserId()
                }) ;

                return response;
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message);

                return new GetConversationsResponse(); 
            }
        }

        public IActionResult CreateQuery(CreateTicket queryRequest)
        {
            try
            {
                var response = _grpcClients.QueryServiceClient.CreateQuery(new CreateQueryRequest
                {
                    QueryTitle = queryRequest.QueryTitle,
                    QueryDescription = queryRequest.QueryDescription,
                    QueryType = queryRequest.QueryType,
                    CreatedId = GetCurrentUserId()
                });

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public GetTicketsResponse GetTicketsForAdmin()
        {
            try
            {
                var response = _grpcClients.QueryServiceClient.GetTicketsForAdmin(new Google.Protobuf.WellKnownTypes.Empty());

                return (response);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return new GetTicketsResponse();
            }
        }

        public GetTicketsResponse GetTicketsForUser()
        {
            try
            {
                var response = _grpcClients.QueryServiceClient.GetTicketsForUser(new CommonRequest
                {
                    RequestId = GetCurrentUserId()
                });

                return (response);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return new GetTicketsResponse();
            }
        }
        public IActionResult CloseTicket(CommonRequest request)
        {
            try
            {
                var response = _grpcClients.QueryServiceClient.CloseTicket(request);

                return new OkObjectResult(response);
            }
            catch (RpcException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
