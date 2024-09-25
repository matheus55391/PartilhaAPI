using System;
using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.Models
{
    public class Friend
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid FriendId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual User FriendUser { get; set; }
    }
}
