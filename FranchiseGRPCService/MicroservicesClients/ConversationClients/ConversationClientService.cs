using ChatGRPCService.Protos;
using ChatPackage;
using FranchiseGRPCService.GrpcServicesClient;

namespace FranchiseGRPCService.MicroservicesClients.ConversationClients
{
    public class ConversationClientService : IConversationClientService
    {
        private readonly IGRPCClients _gRPCClients;

        public ConversationClientService(IGRPCClients gRPCClients) 
        {
            _gRPCClients = gRPCClients;
        }

        public CreateConversationResponse CreateConversation(CreateConversationRequest conversationRequest)
        {
            CreateConversationResponse response = _gRPCClients.ConversationServiceClient.CreateConversation(conversationRequest);
            return response;
        }

        public CommonResponse UpdateConversationStatus(UpdateAcceptedStatusRequest updateStatusRequest)
        {
            CommonResponse response = _gRPCClients.ConversationServiceClient.UpdateAcceptedStatus(updateStatusRequest);
            return response;
        }
    }
}
