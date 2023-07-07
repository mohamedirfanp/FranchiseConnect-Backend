using FranchiseGRPCService.Protos;
using FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FranchiseGRPCService.Services
{
    public class franchiseService : FranchiseService.FranchiseServiceBase
    {
        private readonly IFranchiseServiceHandler _franchiseServiceHandler;

        public franchiseService(IFranchiseServiceHandler franchiseServiceHandler) 
        { 
            _franchiseServiceHandler = franchiseServiceHandler;
        }

        public override Task<CreateFranchiseResponse> CreateFranchise(CreateFranchiseRequest request, ServerCallContext context)
        {

            var response = _franchiseServiceHandler.CreateFranchise(request);


            return Task.FromResult(response);
            
        }

        public override Task<GetFranchiseResponse> GetAllFranchise(Empty request, ServerCallContext context)
        {
            var response = _franchiseServiceHandler.GetAllFranchise();

            return Task.FromResult(response);
        }

        //public override Task<GetFranchisesRespsonse> GetFranchiseById(GetFranchiseByIdRequest request, ServerCallContext context)
        public override Task<GetFranchisesRespsonse> GetFranchiseById(GetFranchiseByIdRequest request, ServerCallContext context)
        {
            var response = _franchiseServiceHandler.GetFranchiseById(request);
            return Task.FromResult(response);
        }

        public override Task<FranchiseExistResponse> FranchiseExist(FranchiseExistRequest request, ServerCallContext context)
        {
            var response = _franchiseServiceHandler.FranchiseExists(request);
            return Task.FromResult(response);
        }

        public override Task<IncreseViewCountResponse> IncreseViewCount(IncreseViewCountRequest request, ServerCallContext context)
        {
            var response = _franchiseServiceHandler.IncrementViewCount(request);
            return Task.FromResult(response);
        }

    }
}
