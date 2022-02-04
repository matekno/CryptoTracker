using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class BalanceHistory
    {
        public int IdBalanceHistory { get; set; }
        public int FkWallet { get; set; }
        public int FkToken { get; set; }
        public int Balance { get; set; }
        public DateTime Date { get; set; }
        public int OldPrice { get; set; }

        public virtual Token FkTokenNavigation { get; set; } = null!;
        public virtual Wallet FkWalletNavigation { get; set; } = null!;
    }
}
