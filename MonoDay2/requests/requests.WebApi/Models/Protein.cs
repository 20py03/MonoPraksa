using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace requests.WebApi.Models
{
    public class Protein
    {
        public int Id {  get; set; }
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }

        public Protein(int id, string flavor, double price, int weight) { 
            Id = id;
            Flavor = flavor;
            Price = price;
            Weight = weight;
        }
    }
}