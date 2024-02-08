using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace requests.WebApi.Models
{
    public class CreateProtein
    {
        public Guid Id { get; set; } 
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }

        public Guid CategoryId { get; set; }


        public CreateProtein(Guid id, string flavor, double price, int weight, Guid categoryId)
        {
            this.Id = id;
            this.Flavor = flavor;
            this.Price = price;
            this.Weight = weight;
            CategoryId = categoryId;
        }
    }
}