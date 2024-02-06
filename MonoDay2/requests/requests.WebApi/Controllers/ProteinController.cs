using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using requests.WebApi.Models;
using System.Net.Http;
using System.Net;

namespace requests.WebApi.Controllers
{
    public class ProteinController : ApiController
    {
        //static - postoji samo jedna instanca objekta
        private static List<Protein> _proteinList = new List<Protein>();


        /*
        // GET: Protein
        public HttpResponseMessage GetProteinList()
        {
            if (_proteinList != null && _proteinList.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, _proteinList);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Protein list is empty");
            }
        }
        */

        //GET : Get protein with filters
        public HttpResponseMessage GetProteinList(string flavor = null, double price = 0, int weight = 0)
        {
            var filteredProteins = _proteinList;

            if (flavor != null)
            {
                filteredProteins = filteredProteins.Where(p => p.Flavor.Equals(flavor)).ToList();
            }

            if (price != 0)
            {
                filteredProteins = filteredProteins.Where(p => p.Price == price).ToList();
            }

            if (weight != 0)
            {
                filteredProteins = filteredProteins.Where(p => p.Weight == weight).ToList();
            }

            List<GetProtein> getProtein = new List<GetProtein>();
            foreach(Protein protein in filteredProteins)
            {
                getProtein.Add(new GetProtein(protein.Flavor, protein.Price, protein.Weight));
            }

            if (getProtein.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, getProtein);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No proteins match the specified criteria.");
            }
        }

        //GET : Get protein by id
        public HttpResponseMessage GetProteinById(int id)
        {
            Protein protein = _proteinList.FirstOrDefault(p => p.Id == id);

            if (protein != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, protein);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Protein with specified ID not found.");
            }
        }

        // POST : Add new protein
        public HttpResponseMessage PostAddNewProtein(CreateProtein protein)
        {
            if (protein == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty");
            }
            else
            {
                int id = 0;
                if (_proteinList.Count == 0)
                {
                    id = 1;
                }
                else
                {
                    int index = _proteinList.Max(p => p.Id);
                    id = index + 1;
                }

                _proteinList.Add(new Protein(id,protein.Flavor,protein.Price,protein.Weight));
                return Request.CreateResponse(HttpStatusCode.Created, $"Protein {protein.Flavor} successfully added");
            }
        }

        //PUT : Update protein price
        public HttpResponseMessage PutPriceById(int id, [FromBody] UpdateProtein update)
        {
            Protein proteinToUpdate = _proteinList.FirstOrDefault(p => p.Id == id);

            if (proteinToUpdate != null)
            {
                proteinToUpdate.Price = update.Price;
                return Request.CreateResponse(HttpStatusCode.OK, _proteinList);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Protein with specified ID not found.");
            }
        }

        //DELETE : Delete protein by id
        public HttpResponseMessage DeleteFromList(int id)
        {
            Protein proteinToRemove = _proteinList.FirstOrDefault(p => p.Id == id);

            if (proteinToRemove != null)
            {
                _proteinList.Remove(proteinToRemove);
                return Request.CreateResponse(HttpStatusCode.OK, _proteinList);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Protein with specified ID not found.");
            }
        }

    }
}
