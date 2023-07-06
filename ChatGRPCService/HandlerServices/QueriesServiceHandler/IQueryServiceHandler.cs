using ChatGRPCService.Protos;
using ChatPackage;
using System.Runtime.CompilerServices;

namespace ChatGRPCService.HandlerServices.QueriesServiceHandler
{
    public interface IQueryServiceHandler
    {

        // A function to create a query
        CommonResponse CreateQuery(CreateQueryRequest queryRequest);

        // A function to get ticket for user
        GetTicketsResponse GetTicketsForUser(CommonRequest request);

        // A function to get ticket for admin
        GetTicketsResponse GetTicketsForAdmin();

        // A function to close the ticket
        CommonResponse CloseTicket(CommonRequest request);

    }
}
