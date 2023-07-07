using Franchise;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseRequestHandlers
{
    public interface IFranchiseRequestHandler
    {
        // A function to create request for a Franchisee
        FranchiseUserResponse CreateFranchiseRequest(CreateFranchiseUserRequest userRequest);

        // A function to get all request for a Franchisor

        FranchiseRequestResponseList GetFranchiseRequest(GetFranchiseRequestByUserID userID);

        FranchiseRequestResponseList GetFranchiseAllRequest(GetFranchiseRequestByUserID userID);


        FranchiseUserResponse UpdateRequestStatus(UpdateStatusRequest updateStatusRequest);

    }
}
