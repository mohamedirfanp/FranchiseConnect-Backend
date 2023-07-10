using ChatGRPCService.Protos;
using ChatPackage;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseGRPCService.MicroservicesClients.ConversationClients
{
    public interface IConversationClientService
    {

        // A function to Create Conversation
        CreateConversationResponse CreateConversation(CreateConversationRequest conversationRequest);

        // A function to Update Conversation Status

        CommonResponse UpdateConversationStatus(UpdateAcceptedStatusRequest updateStatusRequest);
    }
}
