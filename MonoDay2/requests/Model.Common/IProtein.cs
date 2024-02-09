using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface IProtein
    {
        Guid Id { get; set; }
        string Flavor { get; set; }
        double Price { get; set; }
        int Weight { get; set; }
        Guid CategoryId { get; set; }
    }
}
