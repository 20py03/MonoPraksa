using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.Common;

namespace requests.Model
{
    public class GetProteinWithCategory : IGetProteinWithCategory
    {
        public string Flavor { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }
        public bool IsVegan { get; set; }
        public bool IsAnabolic { get; set; }
        public bool IsRecovery { get; set; }

        public GetProteinWithCategory(string flavor, double price, int weight, bool isVegan, bool isAnabolic, bool isRecovery)
        {
            Flavor = flavor;
            Price = price;
            Weight = weight;
            IsVegan = isVegan;
            IsAnabolic = isAnabolic;
            IsRecovery = isRecovery;
        }

    }
}