using ChatGRPCService.HandlerServices.QueriesServiceHandler;
using ChatGRPCService.Protos;
using ChatPackage;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ChatGRPCService.Services
{
    public class QueriesService : QueryService.QueryServiceBase
    {
        private readonly IQueryServiceHandler _queryServiceHandler;

        public QueriesService(IQueryServiceHandler queryServiceHandler)
        {
            _queryServiceHandler = queryServiceHandler;
        }

        public override Task<CommonResponse> CreateQuery(CreateQueryRequest request, ServerCallContext context)
        {
            var response = _queryServiceHandler.CreateQuery(request);

            return Task.FromResult(response);
        }

        public override Task<GetTicketsResponse> GetTicketsForUser(CommonRequest request, ServerCallContext context)
        {
            var response = _queryServiceHandler.GetTicketsForUser(request);

            return Task.FromResult(response);
        }

        public override Task<GetTicketsResponse> GetTicketsForAdmin(Empty request, ServerCallContext context)
        {
            var response = _queryServiceHandler.GetTicketsForAdmin();

            return Task.FromResult(response);

        }

        public override Task<CommonResponse> CloseTicket(CommonRequest request, ServerCallContext context)
        {
            var response = _queryServiceHandler.CloseTicket(request);

            return Task.FromResult(response);
        }

    }
}
