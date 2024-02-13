using requests.SortingPaging.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace requests.SortingPaging.Common
{
    public class Filtering
    {
        public bool? IsVegan { get; set; }
        public bool? IsAnabolic { get; set; }
        public bool? IsRecovery { get; set; }
        public string Flavor { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? MinWeight { get; set; }
        public int? MaxWeight { get; set; }

        public Filtering (bool? isVegan, bool? isAnabolic, bool? isRecovery, string flavor, double? minPrice, double? maxPrice, int? minWeight, int? maxWeight)
        {
            this.IsVegan = isVegan;
            this.IsAnabolic = isAnabolic;
            this.IsRecovery = isRecovery;
            this.Flavor = flavor;
            this.MinPrice = minPrice;
            this.MaxPrice = maxPrice;
            this.MinWeight = minWeight;
            this.MaxWeight = maxWeight;
        }
    }
}
