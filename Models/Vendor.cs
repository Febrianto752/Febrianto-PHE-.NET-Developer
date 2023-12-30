using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Enums;

namespace Models
{
    [Table("vendors")]
    public class Vendor : BaseEntity
    {
        [Column("business_field")]
        public string? BusinessField { get; set; }

        [Column("type_company")]
        public string? TypeCompany { get; set; }

        [Column("profile_image")]
        public string? ProfileImage { get; set; }

        [Column("status")]
        public VendorStatusEnum Status { get; set; }

        [Column("account_guid"), Required]
        public Guid AccountGuid { get; set; }

        [ForeignKey("AccountGuid"), ValidateNever]
        public Account? Account { get; set; }
    }
}
