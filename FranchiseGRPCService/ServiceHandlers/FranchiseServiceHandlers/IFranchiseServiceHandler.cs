using FranchiseGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers
{
    public interface IFranchiseServiceHandler
    {
        // A function to create franchise
        CreateFranchiseResponse CreateFranchise(CreateFranchiseRequest request);

        // A function to get all the franchise
        GetFranchiseResponse GetAllFranchise();

        // A function to get the franchise by Id
        GetFranchisesRespsonse GetFranchiseById(GetFranchiseByIdRequest request);

    }
}
