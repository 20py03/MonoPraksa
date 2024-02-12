using requests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using requests.Repository;
using Service.Common;

namespace requests.Service
{
    public class ProteinService : IProteinService
    {
        public async Task<int> CreateProteinAsync(Protein protein)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return await proteinRepository.AddNewProteinAsync(protein);
        }

        public async Task<List<GetProteinWithCategory>> GetProteinAsync()
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return await proteinRepository.GetAllProteinsAsync();
        }

        public async Task<int> DeleteProteinByIdAsync(Guid id)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return await proteinRepository.DeleteProteinAsync(id);
        }

        public async Task<List<Protein>> GetByIdAsync(Guid id)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return await proteinRepository.GetProteinByIdAsync(id);
        }

        public async Task<int> PutPriceAsync(Guid id, double price)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return await proteinRepository.PutProteinPriceAsync(id, price);
        }
        
    }
}
