using CodeChallenge.Data;
using System.Threading.Tasks;
using CodeChallenge.DTO;
using System.Linq;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;

        public CompensationRepository(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public CompensationDto Add(CompensationDto compensation)
        {
            _compensationContext.CompensationSet.Add(compensation);
            return compensation;
        }

        public CompensationDto GetById(string id)
        {
            return _compensationContext.CompensationSet.SingleOrDefault(c => c.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
