using AccoutGRPCService.Protos;
using ChatGRPCService.Protos;
using Franchise;
using FranchiseGRPCService.Protos;

namespace ApiGateway.GRPCMicroserviceClients
{
    public interface IGRPCClients
    {
        AuthenticationHandler.AuthenticationHandlerClient AuthClient { get; }
        AccountHandler.AccountHandlerClient AccountClient { get; }
        FranchiseService.FranchiseServiceClient FranchiseClient { get; }
        FranchiseGalleryService.FranchiseGalleryServiceClient FranchiseGalleryClient { get; }
        FranchiseServiceService.FranchiseServiceServiceClient FranchiseProvideServiceClient { get; }
        FranchiseRequestService.FranchiseRequestServiceClient FranchiseRequestClient { get; }

        UserWishListService.UserWishListServiceClient UserWishListServiceClient { get; }

        ConversationService.ConversationServiceClient ConversationServiceClient { get; }

        QueryService.QueryServiceClient QueryServiceClient { get; }

        ChatService.ChatServiceClient ChatServiceClient { get; }

        
    }
}
