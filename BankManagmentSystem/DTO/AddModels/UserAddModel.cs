using System.ComponentModel.DataAnnotations;

namespace BankManagmentSystem.DTO
{
    public class UserAddModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public long MobileNo { get; set; }
        [Required]
        public string EmailId { get; set; } = null!;
        [Required]
        public long AadharNo { get; set; }
        [Required]
        public int Balance { get; set; }
        [Required]
        public string? AccountType { get; set; }
    }
}
