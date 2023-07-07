    using AutoMapper;
using Franchise;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.Models;
using Grpc.Core;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseProvideServiceHandlers
{
    public class FranchiseProvideServiceHandler : IFranchiseProvideServiceHandler
    {
        private readonly FranchiseConnectContext _context;

        public FranchiseProvideServiceHandler(FranchiseConnectContext context)
        {
            _context = context;
        }

        public FranchiseServiceResponse addService(CreateFranchiseServiceRequest createServiceRequest)
        {
            try
            {

                FranchiseServiceModel newService = new FranchiseServiceModel();
                newService.FranchiseProvideServiceName = createServiceRequest.FranchiseProvideServiceName;

                newService.FranchiseProvideServiceDescription = createServiceRequest.FranchiseProvideServiceDescription;

                newService.FranchiseId = createServiceRequest.FranchiseId;

                _context.FranchiseServiceModel.Add(newService);

                _context.SaveChanges();

                return new FranchiseServiceResponse()
                {
                    Response = "Successfully Added Service"
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public FranchiseServiceResponse deleteService(DeleteFranchiseServiceRequest deleteServiceRequest)
        {
            try
            {

                var franchiseProvidedService = _context.FranchiseServiceModel.Find(deleteServiceRequest.FranchiseProvideServiceId);

                if (franchiseProvidedService == null)
                {
                    return new FranchiseServiceResponse
                    {
                        Response = "Service Not Found"
                    };
                }

                _context.FranchiseServiceModel.Remove(franchiseProvidedService);
                _context.SaveChanges();

                return new FranchiseServiceResponse()
                {
                    Response = "Successfully Deleted Service"
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }
    }
}
