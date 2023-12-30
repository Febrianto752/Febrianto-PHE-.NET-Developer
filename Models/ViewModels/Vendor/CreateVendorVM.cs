using Microsoft.AspNetCore.Http;
using Models.ViewModels.Account;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Vendor
{
    public class CreateVendorVM
    {
        [Required]
        public IFormFile ProfileImage { get; set; }

        public CreateAccountVM CreateAccountVM { get; set; }
    }
}
