using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class Chain
    {
        public Chain()
        {
            Tokens = new HashSet<Token>();
        }

        public int IdChain { get; set; }
        public string Name { get; set; } = null!;
        public string? RpcUrl { get; set; }
        public int? ChainId { get; set; }
        public string? BlockExplorerUrl { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}
