using AccoutGRPCService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccoutGRPCService.Data
{
    public class FranchiseConnectContext : DbContext
    {
        public FranchiseConnectContext(DbContextOptions<FranchiseConnectContext> options) : base(options)
        { }

        public DbSet<UserModel> User { get; set; } = default!;

    }
}
