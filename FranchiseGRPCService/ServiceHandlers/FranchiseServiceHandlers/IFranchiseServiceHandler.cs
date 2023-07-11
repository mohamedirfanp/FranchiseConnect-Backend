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
        
        // A function to get the franchise by Id
        GetFranchisesRespsonse GetFranchiseByOwnerId(GetFranchiseByOwnerIdRequest request);

        // A function to update the franchise details
        UpdateFranchiseDetailResponse UpdateFranchise(UpdateFranchiseDetailRequest request);


        // A function to know franchise exist for the user
        FranchiseExistResponse FranchiseExists(FranchiseExistRequest request);

        // A function to increament the view count
        IncreseViewCountResponse IncrementViewCount(IncreseViewCountRequest request);

    }
}
