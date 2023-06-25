using FranchiseGRPCService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class franchiseService : FranchiseService.FranchiseServiceBase
    {
        public override Task<CreateFranchiseResponse> CreateFranchise(CreateFranchiseRequest request, ServerCallContext context)
        {
            return base.CreateFranchise(request, context);
        }

        public override Task<GetFranchiseResponse> GetAllFranchise(Empty request, ServerCallContext context)
        {
            return base.GetAllFranchise(request, context);
        }

        public override Task<GetFranchisesRespsonse> GetFranchiseById(GetFranchiseByIdRequest request, ServerCallContext context)
        {
            return base.GetFranchiseById(request, context);
        }
    }
}
