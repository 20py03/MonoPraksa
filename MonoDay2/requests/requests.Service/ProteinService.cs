using requests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using requests.Repository;
using Service.Common;
using Repository.Common;
using requests.SortingPaging.Common;

namespace requests.Service
{
    public class ProteinService : IProteinService
    {
        private IProteinRepository _proteinRepository;

        public ProteinService(IProteinRepository proteinRepository)
        {
            _proteinRepository = proteinRepository;
        }

        public async Task<int> CreateProteinAsync(Protein protein)
        {
            return await _proteinRepository.AddNewProteinAsync(protein);
        }

        public async Task<List<GetProteinWithCategory>> GetProteinAsync(Filtering filtering, Sorting sorting, Paging paging)
        {
            return await _proteinRepository.GetAllProteinsAsync(filtering, sorting, paging);
        }

        public async Task<int> DeleteProteinByIdAsync(Guid id)
        {
            return await _proteinRepository.DeleteProteinAsync(id);
        }

        public async Task<List<Protein>> GetByIdAsync(Guid id)
        {
            return await _proteinRepository.GetProteinByIdAsync(id);
        }

        public async Task<int> PutPriceAsync(Guid id, double price)
        {
            return await _proteinRepository.PutProteinPriceAsync(id, price);
        }
    }
}
