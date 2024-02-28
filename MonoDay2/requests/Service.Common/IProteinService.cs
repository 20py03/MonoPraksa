using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using requests.Model;
using requests.SortingPaging.Common;

namespace Service.Common
{
    public interface IProteinService
    {
        Task<int> CreateProteinAsync(Protein protein);
        Task<PagedList<GetProteinWithCategory>> GetProteinAsync(Filtering filtering, Sorting sorting, Paging paging);
        Task<int> DeleteProteinByIdAsync(Guid id);
        Task<List<Protein>> GetByIdAsync(Guid id);
        Task<int> PutPriceAsync(Guid id, double price);

        Task<List<Category>> GetCategoryListAsync();
        Task<int> AddCategoryNameByIdAsync(Guid id, string categoryName);
    }
}
