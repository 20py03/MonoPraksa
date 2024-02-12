using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface IGetProteinWithCategory
    {
        string Flavor { get; set; }
        double Price { get; set; }
        int Weight { get; set; }
        bool IsVegan { get; set; }
        bool IsAnabolic { get; set; }
        bool IsRecovery { get; set; }

    }
}
