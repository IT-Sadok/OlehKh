using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE_Project_1.Models
{
    [Table(nameof(Account))]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
