using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("projects")]
    public class Project : BaseEntity
    {
        [Required, Column("name")]
        public string Name { get; set; }

        [Required, Column("description")]
        public string Description { get; set; }

        [Required, Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required, Column("end_date")]
        public DateTime EndDate { get; set; }

        public ICollection<ProjectTender>? ProjectTenders { get; set; }

    }
}
