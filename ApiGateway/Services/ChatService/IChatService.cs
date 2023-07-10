using ApiGateway.Dto_Models;
using ChatGRPCService.Protos;
using ChatPackage;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.ChatService
{
    public interface IChatService
    {
        // A function to create a conversation
        IActionResult CreateConversation(CreateConversationRequest conversationRequest);

        // A function to get Conversations 
        GetConversationsResponse GetConversations();

        // A function to create a query
        IActionResult CreateQuery(CreateTicket queryRequest);

        // A function to get ticket for user
        GetTicketsResponse GetTicketsForUser();

        // A function to get ticket for admin
        GetTicketsResponse GetTicketsForAdmin();

        // A function to close the ticket
        IActionResult CloseTicket(CommonRequest request);
    }
}
