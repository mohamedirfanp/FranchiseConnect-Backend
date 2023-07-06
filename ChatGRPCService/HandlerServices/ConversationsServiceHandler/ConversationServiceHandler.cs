using ChatGRPCService.Data;
using ChatGRPCService.Models;
using ChatGRPCService.Protos;
using ChatPackage;
using Grpc.Core;

namespace ChatGRPCService.HandlerServices.ConversationsServiceHandler
{
    public class ConversationServiceHandler : IConversionServiceHandler
    {
        private readonly FranchiseConnectContext _context;

        public ConversationServiceHandler(FranchiseConnectContext context)
        {
            _context = context;
        }

        // To create a conversation between Members
        public CommonResponse CreateConversation(CreateConversationRequest conversationRequest)
        {
            try
            {
                ConversationModel newConversation = new ConversationModel();
                newConversation.FranchiseeId = conversationRequest.FranchiseeId;
                newConversation.FranchisorId = conversationRequest.FranchisorId;
                newConversation.FranchiseeName = conversationRequest.FranchiseeName;
                newConversation.FranchisorFranchiseName = conversationRequest.FranchisorFranchiseName;

                _context.ConversationModel.Add(newConversation);

                _context.SaveChanges();

                return new CommonResponse
                {
                    Response = "Successfully Conversation Created"
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        // To get the Conversations
        public GetConversationsResponse GetConversations(CommonRequest request)
        {
            try
            {
                var conversationList = _context.ConversationModel.Where(conv => conv.FranchisorId == request.RequestId || conv.FranchiseeId == request.RequestId).ToList();
                GetConversationsResponse getConversations = new GetConversationsResponse();
                foreach (var conversation in conversationList)
                {
                    Conversation getConversation = new Conversation()
                    {
                        FranchiseeId = conversation.FranchiseeId,
                        FranchisorId = conversation.FranchisorId,
                        FranchiseeName = conversation.FranchiseeName,
                        FranchisorFranchiseName = conversation.FranchisorFranchiseName,
                        ConversationId = conversation.ConversationId,
                        IsBlocked = conversation.IsBlocked
                    };

                    getConversations.Conversations.Add(getConversation);

                }

                return getConversations;


            }
            catch(Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
