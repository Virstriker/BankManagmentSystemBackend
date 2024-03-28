namespace BankManagmentSystem.Models
{
    public class JoinModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public long MobileNo { get; set; }
        public string EmailId { get; set; } = null!;
        public long AadharNo { get; set; }
        public int AccountNumber { get; set; }
        public int Balance { get; set; }
        public string? AccountType { get; set; }
    }
}
