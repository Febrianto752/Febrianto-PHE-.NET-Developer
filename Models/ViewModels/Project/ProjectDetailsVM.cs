using Models.ViewModels.ProjectTender;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Project
{
    public class ProjectDetailsVM
    {
        public Guid ProjectGuid { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public List<VendorParticipantVM>? VendorParticipants { get; set; }
    }
}
