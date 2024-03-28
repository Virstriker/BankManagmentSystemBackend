using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public string AccountType1 { get; set; } = null!;
        public int IntrestRate { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
