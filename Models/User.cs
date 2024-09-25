using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }  
    }

}
