using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            BalanceHistories = new HashSet<BalanceHistory>();
            WalletXTokens = new HashSet<WalletXToken>();
        }

        public int IdWallet { get; set; }
        public string Address { get; set; } = null!;
        public string? Nickname { get; set; }
        public string? Type { get; set; }
        public int FkUser { get; set; }

        public virtual User FkUserNavigation { get; set; } = null!;
        public virtual ICollection<BalanceHistory> BalanceHistories { get; set; }
        public virtual ICollection<WalletXToken> WalletXTokens { get; set; }
    }
}
