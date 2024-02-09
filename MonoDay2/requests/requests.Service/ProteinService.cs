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
        public int CreateProtein(Protein protein)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return proteinRepository.AddNewProtein(protein);
        }

        public List<GetProteinWithCategory> GetProtein()
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return proteinRepository.GetAllProteins();
        }

        public int DeleteProteinById(Guid id)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return proteinRepository.DeleteProtein(id);
        }

        public List<Protein> GetById(Guid id)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return proteinRepository.GetProteinById(id);
        }

        public int PutPrice(Guid id, double price)
        {
            ProteinRepository proteinRepository = new ProteinRepository();
            return proteinRepository.PutProteinPrice(id, price);
        }
        
    }
}
