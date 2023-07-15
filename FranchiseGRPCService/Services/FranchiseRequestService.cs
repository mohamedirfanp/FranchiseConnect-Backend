using Franchise;
using FranchiseGRPCService.Protos;
using FranchiseGRPCService.ServiceHandlers.FranchiseRequestHandlers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class FranchiseRequest : FranchiseRequestService.FranchiseRequestServiceBase
    {
        private readonly IFranchiseRequestHandler _franchiseRequestHandler;

        public FranchiseRequest(IFranchiseRequestHandler franchiseRequestHandler)
        {
            _franchiseRequestHandler = franchiseRequestHandler;
        }

        public override Task<FranchiseUserResponse> CreateFranchiseRequest(CreateFranchiseUserRequest request, ServerCallContext context)
        {
            

            var response = _franchiseRequestHandler.CreateFranchiseRequest(request);

            return Task.FromResult(response);
        }

        public override Task<FranchiseRequestResponseList> GetFranchiseRequest(GetFranchiseRequestByUserID request, ServerCallContext context)
        {
            var response = _franchiseRequestHandler.GetFranchiseRequest(request);

            return Task.FromResult(response);
        }


        public override Task<FranchiseUserResponse> UpdateStatus(UpdateStatusRequest request, ServerCallContext context)
        {
            Console.WriteLine("HERE " + request);
            var response = _franchiseRequestHandler.UpdateRequestStatus(request);

            return Task.FromResult(response);
        }

        public override Task<FranchiseRequestResponseList> GetFranchiseRequests(GetFranchiseRequestByUserID request, ServerCallContext context)
        {
            var response = _franchiseRequestHandler.GetFranchiseAllRequest(request);

            return Task.FromResult(response);
        }

    }
}
