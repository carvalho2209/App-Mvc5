using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AAC.AppMvc.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(50, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        [DisplayName("Number")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string Street { get; set; }

        [StringLength(50, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 1)]
        [DisplayName("Apt")]
        public string AptNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string State { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(10, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 5)]
        [DisplayName("Zip Code")]
        public string PostalCode { get; set; }

        [HiddenInput]
        public Guid ProviderId { get; set; }
    }
}