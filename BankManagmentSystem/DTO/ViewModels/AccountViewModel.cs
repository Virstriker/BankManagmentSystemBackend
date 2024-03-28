namespace BankManagmentSystem.DTO.ViewModels
{
    public class AccountViewModel
    {
        public int AccountNumber { get; set; }
        public int AccountUser { get; set; }
        public int Balance { get; set; }
        public string? AccountType { get; set; }
        public bool? Active { get; set; }
    }
}
