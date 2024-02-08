using Npgsql.Internal.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace requests.WebApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public bool IsVegan { get; set; }

        public bool IsString { get; set; }

        public bool IsRecovery { get; set; }

        public Category (Guid id, bool isVegan, bool isString, bool isRecovery)
        {
            Id = id;
            IsVegan = isVegan;
            IsString = isString;
            IsRecovery = isRecovery;
        }
    }
}