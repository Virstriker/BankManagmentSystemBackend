using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            MoneyTransactionTransactionAccountNavigations = new HashSet<MoneyTransaction>();
            MoneyTransactionTransactionToAccountNavigations = new HashSet<MoneyTransaction>();
        }

        public int AccountNumber { get; set; }
        public int AccountUser { get; set; }
        public int Balance { get; set; }
        public string? AccountType { get; set; }
        public bool? Active { get; set; }

        public virtual AccountType? AccountTypeNavigation { get; set; }
        public virtual BankUser AccountUserNavigation { get; set; } = null!;
        public virtual ICollection<MoneyTransaction> MoneyTransactionTransactionAccountNavigations { get; set; }
        public virtual ICollection<MoneyTransaction> MoneyTransactionTransactionToAccountNavigations { get; set; }
    }
}
