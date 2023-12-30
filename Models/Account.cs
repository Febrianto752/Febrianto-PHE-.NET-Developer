using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("accounts")]
    public class Account : BaseEntity
    {
        [EmailAddress, Required, Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column("password", TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column("name"), Required]
        public string Name { get; set; }

        [Column("no_telp"), Required]
        public string NoTelp { get; set; }

        [Column("role"), Required] // Admin, Manager, Vendor
        public string Role { get; set; }
    }
}
