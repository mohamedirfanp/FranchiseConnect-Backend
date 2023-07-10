using AccoutGRPCService.Protos;

namespace FranchiseGRPCService.MicroservicesClients.AccountClient
{
    public interface IAccountClientService
    {
        // A function to get user profile
        GetProfileResponse GetProfile(int request_id);
    }
}
