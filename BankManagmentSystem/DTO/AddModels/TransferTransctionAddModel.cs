namespace BankManagmentSystem.DTO
{
    public class TransferTransctionAddModel
    {
        public int TransactionAccount { get; set; }
        public int? TransactionToAccount { get; set; }
        public int TransactionAmount { get; set; }
    }
}
