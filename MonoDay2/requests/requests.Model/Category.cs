using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Model.Common;

namespace requests.Model
{
    public class Category : ICategory
    {

        public Guid Id { get; set; }
        public bool IsVegan { get; set; }

        public bool IsAnabolic { get; set; }

        public bool IsRecovery { get; set; }

        public string Name { get; set; }

        public Category (Guid id, bool isVegan, bool isAnabolic, bool isRecovery, string name)
        {
            Id = id;
            IsVegan = isVegan;
            IsAnabolic = isAnabolic;
            IsRecovery = isRecovery;
            Name = name;
        }

        public Category(bool isVegan, bool isAnabolic, bool isRecovery, string name)
        {
            IsVegan = isVegan;
            IsAnabolic = isAnabolic;
            IsRecovery = isRecovery;
            Name = name;
        }

    }
}