using CodeChallenge.DTO;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        CompensationDto Add(CompensationDto compensation);
        CompensationDto GetById(string id);
        Task SaveAsync();
    }
}
