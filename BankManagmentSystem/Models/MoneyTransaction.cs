using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class MoneyTransaction
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int TransactionAccount { get; set; }
        public int? TransactionToAccount { get; set; }
        public string TransactionType { get; set; } = null!;
        public int TransactionAmount { get; set; }

        public virtual UserAccount TransactionAccountNavigation { get; set; } = null!;
        public virtual UserAccount? TransactionToAccountNavigation { get; set; }
    }
}
