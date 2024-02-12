using requests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IProteinRepository
    {
        Task<int> AddNewProteinAsync(Protein protein);
        Task<List<GetProteinWithCategory>> GetAllProteinsAsync();
        Task<int> DeleteProteinAsync(Guid id);
        Task<List<Protein>> GetProteinByIdAsync(Guid id);
        Task<int> PutProteinPriceAsync(Guid id, double price);
    }
}
