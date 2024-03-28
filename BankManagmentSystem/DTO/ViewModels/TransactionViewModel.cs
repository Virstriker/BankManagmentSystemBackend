namespace BankManagmentSystem.DTO.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int TransactionAccount { get; set; }
        public int? TransactionToAccount { get; set; }
        public string TransactionType { get; set; } = null!;
        public int TransactionAmount { get; set; }
    }
}
