using ChatGRPCService.Protos;
using ChatPackage;

namespace ChatGRPCService.HandlerServices.ConversationsServiceHandler
{
    public interface IConversionServiceHandler
    {

        // A function to create a conversation
        CreateConversationResponse CreateConversation(CreateConversationRequest conversationRequest);

        // A function to get Conversations 
        GetConversationsResponse GetConversations(CommonRequest request);

        // A function to update the Accept Status
        CommonResponse UpdateAcceptStatus (UpdateAcceptedStatusRequest request);

    }
}
