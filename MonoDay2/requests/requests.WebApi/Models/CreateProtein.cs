using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace requests.WebApi.Models
{
    public class CreateProtein
    {
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }

        public CreateProtein(string flavor, double price, int weight)
        {
            this.Flavor = flavor;
            this.Price = price;
            this.Weight = weight;
        }
    }
}