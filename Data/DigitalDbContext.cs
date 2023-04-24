using Microsoft.EntityFrameworkCore;
using DysonDigitalDashboard.Models.Domain;

namespace DysonDigitalDashboard.Data
{
    public class DigitalDbContext : DbContext
    {
        public DigitalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<DysonDigitalDashboard.Models.Domain.ScoreList> ScoreList { get; set; } = default!;
    }
}
