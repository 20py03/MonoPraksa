using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal interface IProteinService
    {
        HttpResponseMessage AddNewProtein(CreateProtein protein);
    }
}
