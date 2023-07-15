using ChatGRPCService.Data;
using ChatGRPCService.Models;
using ChatGRPCService.Protos;
using ChatPackage;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System;

namespace ChatGRPCService.HandlerServices.QueriesServiceHandler
{
    public class QueryServiceHandler : IQueryServiceHandler
    {
        private readonly FranchiseConnectContext _context;

        public QueryServiceHandler(FranchiseConnectContext context)
        {
            _context = context;
        }

        public CommonResponse CreateQuery(CreateQueryRequest queryRequest)
        {
            try
            {
                QueryModel newQuery = new QueryModel(); 
                newQuery.QueryTitle = queryRequest.QueryTitle;
                newQuery.QueryDesciption = queryRequest.QueryDescription;
                newQuery.QueryType = queryRequest.QueryType;
                newQuery.CreatedId = queryRequest.CreatedId;
                newQuery.CreatedAt = DateTime.Now;
                newQuery.Status = true;

                _context.QueryModel.Add(newQuery);

                _context.SaveChanges();

                return new CommonResponse
                {
                    Response = "Successfully Query Created",
                    ResponseId = newQuery.QueryId
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
        public GetTicketsResponse GetTicketsForUser(CommonRequest request)
        {
            var ticketsListForUser = _context.QueryModel.Where(query => query.CreatedId == request.RequestId).ToList();

            GetTicketsResponse getTickets = new GetTicketsResponse();

            foreach (var ticket in ticketsListForUser)
            {
                Query getQuery = new Query()
                {
                    QueryTitle = ticket.QueryTitle,
                    QueryDescription = ticket.QueryDesciption,
                    QueryType = ticket.QueryType,
                    QueryId = ticket.QueryId,
                    CreatedAt = Timestamp.FromDateTime(ticket.CreatedAt.ToUniversalTime()),
                    Status = ticket.Status,
                    CreatedId = ticket.CreatedId,
                };

                getTickets.Tickets.Add(getQuery);
            }

            return getTickets;

        }

        public GetTicketsResponse GetTicketsForAdmin()
        {
            var ticketsListForUser = _context.QueryModel.ToList();

            GetTicketsResponse getTickets = new GetTicketsResponse();

            foreach (var ticket in ticketsListForUser)
            {
                Query getQuery = new Query()
                {
                    QueryTitle = ticket.QueryTitle,
                    QueryDescription = ticket.QueryDesciption,
                    QueryType = ticket.QueryType,
                    QueryId = ticket.QueryId,
                    CreatedAt = Timestamp.FromDateTime(ticket.CreatedAt.ToUniversalTime()),
                    Status = ticket.Status,
                    CreatedId = ticket.CreatedId,
                };

                getTickets.Tickets.Add(getQuery);
            }

            return getTickets;
        }

        public CommonResponse CloseTicket(CommonRequest request)
        {
            try
            {
                var currentTicket = _context.QueryModel.Where(ticket => ticket.QueryId == request.RequestId).FirstOrDefault();

                currentTicket.Status = false;

                _context.QueryModel.Update(currentTicket);

                _context.SaveChanges();

                return new CommonResponse
                {
                    Response = "Successfully Ticket Closed"
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
