using FranchiseGRPCService.Protos;
using FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

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

            Console.WriteLine("Franchise Name " + request.Franchise.FranchiseName);
            Console.WriteLine("Franchise Social Website "+ request.FrachiseSocial.FranchiseWebsite);

            var response = _franchiseServiceHandler.CreateFranchise(request);

            return Task.FromResult(response);
            
        }

        public override Task<GetFranchiseResponse> GetAllFranchise(Empty request, ServerCallContext context)
        {
            return base.GetAllFranchise(request, context);
        }

        //public override Task<GetFranchisesRespsonse> GetFranchiseById(GetFranchiseByIdRequest request, ServerCallContext context)
        public override Task<GetFranchisesRespsonse> GetFranchiseById(GetFranchiseByIdRequest request, ServerCallContext context)
        {
            var response = _franchiseServiceHandler.GetFranchiseById(request);
            throw new NotImplementedException();
        }
    }
}
