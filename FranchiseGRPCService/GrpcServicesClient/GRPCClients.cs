using AccoutGRPCService.Protos;
using ChatGRPCService.Protos;
using Grpc.Net.Client;


namespace FranchiseGRPCService.GrpcServicesClient
{
    public class GRPCClients : FranchiseGRPCService.GrpcServicesClient.IGRPCClients
    {
        public GRPCClients()
        {
            var accountGRPCServiceChannel = GrpcChannel.ForAddress("https://localhost:7212");
            AccountClient = new AccountHandler.AccountHandlerClient(accountGRPCServiceChannel);

            var chatGRPCServiceChannel = GrpcChannel.ForAddress("https://localhost:7291");
            ConversationServiceClient = new ConversationService.ConversationServiceClient(chatGRPCServiceChannel);

        }
        public AccountHandler.AccountHandlerClient AccountClient { get; }

        public ConversationService.ConversationServiceClient ConversationServiceClient { get; }
    }

}
