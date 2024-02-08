using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace requests.WebApi.Models
{
    public class Protein
    {
        public int Id { get; set; }
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }
        public Guid CategoryId { get; set; }

        public Protein(int id, string flavor, double price, int weight, Guid categoryId)
        {
            Id = id;
            Flavor = flavor;
            Price = price;
            Weight = weight;
            CategoryId = categoryId;
        }
    }
}