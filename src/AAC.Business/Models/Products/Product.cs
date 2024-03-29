﻿using AAC.Business.Core.Models;
using AAC.Business.Models.Providers;
using System;

namespace AAC.Business.Models.Products
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Amount { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        /*EF Relations*/
        public Provider Provider { get; set; }  
    }
}
