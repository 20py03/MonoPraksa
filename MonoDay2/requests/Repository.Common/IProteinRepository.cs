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
        int AddNewProtein(Protein protein);
        List<GetProteinWithCategory> GetAllProteins();
        int DeleteProtein(Guid id);
        List<Protein> GetProteinById(Guid id);
        int PutProteinPrice(Guid id, double price);
    }
}
