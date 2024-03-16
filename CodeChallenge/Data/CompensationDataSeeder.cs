using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.DTO;

namespace CodeChallenge.Data
{
    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;
        private const string COMPENSATION_SEED_DATA_FILE = "resources/CompensationSeedData.json";

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if (!_compensationContext.CompensationSet.Any())
            {
                List<CompensationDto> employees = LoadCompensation();
                _compensationContext.CompensationSet.AddRange(employees);

                await _compensationContext.SaveChangesAsync();
            }
        }

        private List<CompensationDto> LoadCompensation()
        {
            using (FileStream fs = new FileStream(COMPENSATION_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                List<CompensationDto> dto = serializer.Deserialize<List<CompensationDto>>(jr);

                return dto;
            }
        }
    }
}
