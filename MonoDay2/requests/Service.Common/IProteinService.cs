using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using requests.Model;
namespace Service.Common
{
    public interface IProteinService
    {
        Task<int> CreateProteinAsync(Protein protein);
        Task<List<GetProteinWithCategory>> GetProteinAsync();
        Task<int> DeleteProteinByIdAsync(Guid id);
        Task<List<Protein>> GetByIdAsync(Guid id);
        Task<int> PutPriceAsync(Guid id, double price);
    }
}
