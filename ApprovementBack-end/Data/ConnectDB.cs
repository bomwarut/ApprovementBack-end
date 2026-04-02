using ApprovementBack_end.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovementBack_end.Data
{
    public class ConnectDB : DbContext
    {
        public ConnectDB(DbContextOptions<ConnectDB>options) : base(options){ }
        public DbSet<ApproveRequest> ApprovelistTable { get; set; }
    }
}
