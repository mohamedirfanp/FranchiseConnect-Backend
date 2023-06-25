using FranchiseGRPCService.Protos;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseServiceHandlers
{
    public interface IFranchiseServiceHandler
    {
        // A function to create franchise
        CreateFranchiseResponse CreateFranchise(CreateFranchiseRequest request);

        // A function to get all the franchise
        GetFranchiseResponse GetFranchise();

        // A function to get the franchise by Id
        GetFranchiseResponse GetFranchiseById(GetFranchiseByIdRequest request);

    }
}
