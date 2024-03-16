using CodeChallenge.DTO;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data
{
    public class CompensationContext : DbContext
    {
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {
        }

        public DbSet<CompensationDto> CompensationSet { get; set; }
    }
}
