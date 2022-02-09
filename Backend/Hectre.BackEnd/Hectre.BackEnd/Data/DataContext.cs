using Hectre.BackEnd.Models;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// The unit of work context for query & manipulating data against database
    /// </summary>
    public class DataContext
    {
        public IEfRepository<Chemical, HectreDbContext> ChemicalRepository { get; }

        public DataContext(IEfRepository<Chemical, HectreDbContext> chemicalRepository)
        {
            ChemicalRepository = chemicalRepository;
        }
    }
}
