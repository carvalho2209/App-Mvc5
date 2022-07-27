using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using AAC.AppMvc.Extensions;

namespace AAC.AppMvc.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [DisplayName("Provider")]
        public Guid ProviderId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(1000, ErrorMessage = "The field {0} needs to be between {2} and {1} caracter", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Product Image")]
        public HttpPostedFileBase ImageUpload { get; set; }

        public string Image { get; set; }

        [Currency]
        [Required(ErrorMessage = "The field {0} is required.")]
        public decimal Amount { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegistrationDate { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }

        public ProviderViewModel Provider { get; set; }

        public IEnumerable<ProviderViewModel> Providers { get; set; }
    }
}