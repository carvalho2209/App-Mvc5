using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AAC.AppMvc.ViewModels
{
    public class ProviderViewModel
    {
        public ProviderViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(9, ErrorMessage = "The field {0} needs to be {1} caracter", MinimumLength = 9)]
        public string Document { get; set; }

        [DisplayName("Type")]
        public int TypeProvider { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }
         
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}