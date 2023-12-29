using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Account
{
    public class EditAccountVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string NoTelp { get; set; }
    }
}
