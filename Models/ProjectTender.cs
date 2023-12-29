using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Enums;

namespace Models
{
    [Table("project_tenders")]
    public class ProjectTender : BaseEntity
    {
        [Required, Column("vendor_guid")]
        public Guid VendorGuid { get; set; }

        [ForeignKey("VendorGuid"), ValidateNever]
        public Vendor? Vendor { get; set; }


        [Required, Column("project_guid")]
        public Guid PorjectGuid { get; set; }

        [ForeignKey("PorjectGuid"), ValidateNever]
        public Project? Project { get; set; }


        [Required, Column("status")]
        public TenderStatusEnum Status { get; set; }
    }
}
