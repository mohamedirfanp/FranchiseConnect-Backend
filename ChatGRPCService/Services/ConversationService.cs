using ChatGRPCService.HandlerServices.ConversationsServiceHandler;
using ChatGRPCService.Protos;
using ChatPackage;
using Grpc.Core;

namespace ChatGRPCService.Services
{
    public class ConversationsService : ConversationService.ConversationServiceBase
    {
        private readonly IConversionServiceHandler _conversationServiceHandler;

        public ConversationsService(IConversionServiceHandler conversionServiceHandler)
        {
            _conversationServiceHandler = conversionServiceHandler;
        }
        public override Task<CommonResponse> CreateConversation(CreateConversationRequest request, ServerCallContext context)
        {
            var response = _conversationServiceHandler.CreateConversation(request);

            return Task.FromResult(response);
        }

        public override Task<GetConversationsResponse> GetConversations(CommonRequest request, ServerCallContext context)
        {
            var response = _conversationServiceHandler.GetConversations(request);

            return Task.FromResult(response);
        }
    }
}
