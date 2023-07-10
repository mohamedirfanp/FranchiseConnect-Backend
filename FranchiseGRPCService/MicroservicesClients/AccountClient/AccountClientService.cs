using AccoutGRPCService.Protos;
using FranchiseGRPCService.GrpcServicesClient;
using Grpc.Core;

namespace FranchiseGRPCService.MicroservicesClients.AccountClient
{
    public class AccountClientService : IAccountClientService
    {
        private readonly IGRPCClients _grpcClients;

        public AccountClientService(IGRPCClients gRPCClients)
        {
            _grpcClients = gRPCClients;
        }

        // A function to get Profile
        public GetProfileResponse GetProfile(int request_id)
        {
            try
            {
                var response = _grpcClients.AccountClient.GetProfile(new GetProfileRequest
                {
                    UserId = request_id
                });

                return response;
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
