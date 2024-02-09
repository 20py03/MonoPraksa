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
        int CreateProtein(Protein protein);
        List<GetProteinWithCategory> GetProtein();
        int DeleteProteinById(Guid id);
        List<Protein> GetById(Guid id);
        int PutPrice(Guid id, double price);
    }
}
