using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class Token
    {
        public Token()
        {
            BalanceHistories = new HashSet<BalanceHistory>();
            WalletXTokens = new HashSet<WalletXToken>();
        }

        public int IdToken { get; set; }
        public int? FkChain { get; set; }
        public string Name { get; set; } = null!;
        public string CgTicker { get; set; } = null!;
        public string? ContractAddress { get; set; }
        public bool IsNative { get; set; }

        public virtual Chain? FkChainNavigation { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistories { get; set; }
        public virtual ICollection<WalletXToken> WalletXTokens { get; set; }
    }
}
