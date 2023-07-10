using AccoutGRPCService.Protos;
using ChatGRPCService.Protos;

namespace FranchiseGRPCService.GrpcServicesClient
{
    public interface IGRPCClients
    {
        
        AccountHandler.AccountHandlerClient AccountClient { get; }

        ConversationService.ConversationServiceClient ConversationServiceClient { get; }
        /*        ConversationService.ConversationServiceClient ConversationServiceClient { get; }

                QueryService.QueryServiceClient QueryServiceClient { get; }

                ChatService.ChatServiceClient ChatServiceClient { get; }*/
    }
}
