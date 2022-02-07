using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class WalletXToken
    {
        public int IdWalletXToken { get; set; }
        public int FkWallet { get; set; }
        public int FkToken { get; set; }
        public double TokenBalance { get; set; }

        public virtual Token FkTokenNavigation { get; set; } = null!;
        public virtual Wallet FkWalletNavigation { get; set; } = null!;
    }
}
