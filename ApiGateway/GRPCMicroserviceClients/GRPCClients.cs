﻿using AccoutGRPCService.Protos;
using ChatGRPCService.Protos;
using Franchise;
using FranchiseGRPCService.Protos;
using Grpc.Net.Client;

namespace ApiGateway.GRPCMicroserviceClients
{
    public class GRPCClients : IGRPCClients
    {
        public GRPCClients()
        {
            var accountGRPCServiceChannel = GrpcChannel.ForAddress("https://localhost:7212");
            AuthClient = new AuthenticationHandler.AuthenticationHandlerClient(accountGRPCServiceChannel);
            AccountClient = new AccountHandler.AccountHandlerClient(accountGRPCServiceChannel);

            var franchiseGPRCServiceChannel = GrpcChannel.ForAddress("https://localhost:7290");
            FranchiseClient = new FranchiseService.FranchiseServiceClient(franchiseGPRCServiceChannel);
            FranchiseGalleryClient = new FranchiseGalleryService.FranchiseGalleryServiceClient(franchiseGPRCServiceChannel);
            FranchiseProvideServiceClient = new FranchiseServiceService.FranchiseServiceServiceClient(franchiseGPRCServiceChannel);
            FranchiseRequestClient = new FranchiseRequestService.FranchiseRequestServiceClient(franchiseGPRCServiceChannel);
            UserWishListServiceClient = new UserWishListService.UserWishListServiceClient(franchiseGPRCServiceChannel);

            var chatGRPCServiceChannel = GrpcChannel.ForAddress("https://localhost:7291");

            ConversationServiceClient = new ConversationService.ConversationServiceClient(chatGRPCServiceChannel);

            QueryServiceClient = new QueryService.QueryServiceClient(chatGRPCServiceChannel);

            ChatServiceClient = new ChatService.ChatServiceClient(chatGRPCServiceChannel);

        }

        public AuthenticationHandler.AuthenticationHandlerClient AuthClient { get; }
        public AccountHandler.AccountHandlerClient AccountClient { get; }
        public FranchiseService.FranchiseServiceClient FranchiseClient { get; }
        public FranchiseGalleryService.FranchiseGalleryServiceClient FranchiseGalleryClient { get; }
        public FranchiseServiceService.FranchiseServiceServiceClient FranchiseProvideServiceClient { get; }
        public FranchiseRequestService.FranchiseRequestServiceClient FranchiseRequestClient { get; }
        public UserWishListService.UserWishListServiceClient UserWishListServiceClient { get; }
        public ChatService.ChatServiceClient ChatServiceClient { get; }
        public ConversationService.ConversationServiceClient ConversationServiceClient { get; }
        public QueryService.QueryServiceClient QueryServiceClient { get; }

    }
}
