using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class NetBanking
    {
        public long AadharNumber { get; set; }
        public string AccountUserId { get; set; } = null!;
        public string? AccountUserPassword { get; set; }

        public virtual BankUser AadharNumberNavigation { get; set; } = null!;
    }
}
