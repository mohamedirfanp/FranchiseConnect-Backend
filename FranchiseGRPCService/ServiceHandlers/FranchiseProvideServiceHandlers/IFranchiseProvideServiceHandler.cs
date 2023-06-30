using Franchise;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseProvideServiceHandlers
{
    public interface IFranchiseProvideServiceHandler
    {
        // A function to addService for existing Franchise
        FranchiseServiceResponse addService(CreateFranchiseServiceRequest createServiceRequest);

        // A function to delete a existing service in the franchise
        FranchiseServiceResponse deleteService(DeleteFranchiseServiceRequest deleteServiceRequest);
    }
}
