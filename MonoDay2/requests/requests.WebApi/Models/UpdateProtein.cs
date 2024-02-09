using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace requests.WebApi.Models
{
    public class UpdateProtein
    {
        public double Price { get; set; }
        public UpdateProtein(double newPrice)
        {
            this.Price = newPrice;
        }
    }
}