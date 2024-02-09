using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface ICategory
    {
        Guid Id { get; set; }
        bool IsVegan { get; set; }
        bool IsAnabolic { get; set; }
        bool IsRecovery { get; set; }
    }
}
