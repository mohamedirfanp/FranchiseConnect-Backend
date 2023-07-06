
using Biddo.Models;
using ChatGRPCService.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatGRPCService.Data
{
    public class FranchiseConnectContext : DbContext
    {
        public FranchiseConnectContext(DbContextOptions<FranchiseConnectContext> options) : base(options)
        { }

        public DbSet<ConversationModel> ConversationModel { get; set; }

        public DbSet<QueryModel> QueryModel { get; set; }

        public DbSet<TimelineCommentModel> TimelineCommentModel { get; set; }

    }
}
