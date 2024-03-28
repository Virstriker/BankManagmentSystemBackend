using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class BankUser
    {
        public BankUser()
        {
            NetBankings = new HashSet<NetBanking>();
            UserAccounts = new HashSet<UserAccount>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public long MobileNo { get; set; }
        public string EmailId { get; set; } = null!;
        public long AadharNo { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<NetBanking> NetBankings { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
