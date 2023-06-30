using Franchise;
using FranchiseGRPCService.ServiceHandlers.FranchiseProvideServiceHandlers;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class FranchiseProvideService : FranchiseServiceService.FranchiseServiceServiceBase
    {
        private readonly IFranchiseProvideServiceHandler _franchiseProvideServiceHandler;

        public FranchiseProvideService(IFranchiseProvideServiceHandler franchiseProvideServiceHandler)
        {
            _franchiseProvideServiceHandler = franchiseProvideServiceHandler;
        }

        public override Task<FranchiseServiceResponse> CreateFranchiseService(CreateFranchiseServiceRequest request, ServerCallContext context)
        {
            var response = _franchiseProvideServiceHandler.addService(request);
            return Task.FromResult(response);
        }

        public override Task<FranchiseServiceResponse> DeleteFranchiseService(DeleteFranchiseServiceRequest request, ServerCallContext context)
        {
            var response = _franchiseProvideServiceHandler.deleteService(request);
            return Task.FromResult(response);
        }
    }
}
