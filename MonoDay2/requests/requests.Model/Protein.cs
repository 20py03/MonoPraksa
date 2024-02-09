using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Model.Common;

namespace requests.Model
{
    public class Protein : IProtein
    {
        public Guid Id { get; set; }
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }
        public Guid CategoryId { get; set; }

        public Protein(Guid id, string flavor, double price, int weight, Guid categoryId)
        {
            Id = id;
            Flavor = flavor;
            Price = price;
            Weight = weight;
            CategoryId = categoryId;
        }

        public Protein(string flavor, double price, int weight, Guid categoryId)
        {
            Flavor = flavor;
            Price = price;
            Weight = weight;
            CategoryId = categoryId;
        }
        public Protein(string flavor, double price, int weight)
        {
            Flavor = flavor;
            Price = price;
            Weight = weight;
       }

    }
}