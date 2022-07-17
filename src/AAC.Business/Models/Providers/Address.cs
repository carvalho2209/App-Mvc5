using AAC.Business.Core.Models;

namespace AAC.Business.Models.Providers
{
    public class Address : Entity 
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string AptNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        /* EF Relations*/
        public Provider Provider { get; set; }

    }
}
