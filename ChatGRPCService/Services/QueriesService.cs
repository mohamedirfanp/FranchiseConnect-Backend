using ChatGRPCService.Protos;
using ChatPackage;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ChatGRPCService.Services
{
    public class QueriesService : QueryService.QueryServiceBase
    {

        public QueriesService() { }

        public override Task<CommonResponse> CreateQuery(CreateQueryRequest request, ServerCallContext context)
        {
            return base.CreateQuery(request, context);
        }

        public override Task<GetTicketsResponse> GetTicketsForUser(CommonRequest request, ServerCallContext context)
        {
            return base.GetTicketsForUser(request, context);
        }

        public override Task<GetTicketsResponse> GetTicketsForAdmin(Empty request, ServerCallContext context)
        {
            return base.GetTicketsForAdmin(request, context);
        }

        public override Task<CommonResponse> CloseTicket(CommonRequest request, ServerCallContext context)
        {
            return base.CloseTicket(request, context);
        }

    }
}
