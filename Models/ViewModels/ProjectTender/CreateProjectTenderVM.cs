using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.ProjectTender
{
    public class CreateProjectTenderVM
    {
        [Required]
        public Guid VendorGuid { get; set; }


        [Required]
        public Guid ProjectGuid { get; set; }
    }
}
