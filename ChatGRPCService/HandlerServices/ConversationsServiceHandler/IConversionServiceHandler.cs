using ChatGRPCService.Protos;
using ChatPackage;

namespace ChatGRPCService.HandlerServices.ConversationsServiceHandler
{
    public interface IConversionServiceHandler
    {

        // A function to create a conversation
        CommonResponse CreateConversation(CreateConversationRequest conversationRequest);

        // A function to get Conversations 
        GetConversationsResponse GetConversations(CommonRequest request);

    }
}
