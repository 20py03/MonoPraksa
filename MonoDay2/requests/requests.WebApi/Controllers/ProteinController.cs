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
using requests.SortingPaging.Common;

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


        public async Task<HttpResponseMessage> GetProteinListAsync(bool? isVegan = null, bool? isAnabolic = null, bool? isRecovery = null, string SortBy = "Price", string SortOrder = "ASC", int PageNumber = 1, int PageSize = 3, string flavor = null, double? minPrice = null, double? maxPrice = null, int? minWeight = null, int? maxWeight = null)
        {
            Sorting sorting = new Sorting(SortBy,SortOrder);
            Paging paging = new Paging(PageNumber, PageSize);
            Filtering filtering = new Filtering(isVegan, isAnabolic, isRecovery, flavor, minPrice, maxPrice, minWeight, maxWeight);

            List<GetProteinWithCategory> proteinList = await _proteinService.GetProteinAsync(filtering, sorting, paging);

            if (proteinList != null && proteinList.Count > 0)
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

        [Route("Api/Protein/Categories")]
        public async Task<HttpResponseMessage> GetCategoryListAsync()
        {
            List<Category> categoryList = await _proteinService.GetCategoryListAsync();

            if (categoryList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No proteins found.");
            }
        }
        [Route("Api/Protein/Categories/{id}")]
        public async Task<HttpResponseMessage> AddCategoryNameByIdAsync([FromUri]Guid id, [FromBody] string categoryName)
        {
           
            int rowsAffected = await _proteinService.AddCategoryNameByIdAsync(id, categoryName);
            if (rowsAffected > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Added category name");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error while adding category name");
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
