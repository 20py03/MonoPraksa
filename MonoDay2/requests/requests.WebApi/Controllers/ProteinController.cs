using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using requests.WebApi.Models;
using System.Net.Http;
using System.Net;
using Npgsql;
using System.Web.UI;
using requests.Service;
using requests.Model;
using GetProteinWithCategory = requests.Model.GetProteinWithCategory;
using System.Threading.Tasks;
using Service.Common;

namespace requests.WebApi.Controllers
{
    public class ProteinController : ApiController
    {
        private IProteinService _proteinService;
        public ProteinController(IProteinService proteinService)
        {
            _proteinService = proteinService;
        }


        public async Task<HttpResponseMessage> PostAddNewProteinAsync(CreateProtein protein)
        {
            Protein proteinToAdd = new Protein(protein.Flavor, protein.Price, protein.Weight, protein.CategoryId);
            int result = await _proteinService.CreateProteinAsync(proteinToAdd);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unable to create a new protein");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Protein created successfully");
        }
        

        public async Task<HttpResponseMessage> GetProteinListAsync()
        {
            List<GetProteinWithCategory> proteinList = await _proteinService.GetProteinAsync();

            if (proteinList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, proteinList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No proteins found.");
            }
        }
        

        public async Task<HttpResponseMessage> GetProteinByIdAsync(Guid id)
        {
            List<Protein> proteinList = await _proteinService.GetByIdAsync(id);

            if (proteinList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, proteinList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No proteins found.");
            }
        }
       

        [HttpPut]
        public async Task<HttpResponseMessage> PutProteinPriceAsync(Guid id, [FromBody] UpdateProtein protein)
        {
            int editResult = await _proteinService.PutPriceAsync(id, protein.Price);

            if (editResult == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, editResult);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No proteins found.");
            }
        }
        

        public async Task<HttpResponseMessage> DeleteProteinAsync(Guid id)
        {
            int deleteResult = await _proteinService.DeleteProteinByIdAsync(id);
            if (deleteResult == 0)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unable to delete protein");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Protein deleted successfully");
        }
        
    }
}
