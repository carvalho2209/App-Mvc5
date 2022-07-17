using AAC.Business.Core.Models;
using System.Collections.Generic;
using AAC.Business.Models.Products;

namespace AAC.Business.Models.Providers 
{
    public class Provider : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public TypeProvider TypeProvider  { get;set;} 
        public Address Address { get; set; }
        public bool Active { get; set; } 

        /* EF Relation*/
        public ICollection<Product> Products { get; set; }
    }
}
