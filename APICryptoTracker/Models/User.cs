using System;
using System.Collections.Generic;

namespace ApiCryptoTracker.Models
{
    public partial class User
    {
        public User()
        {
            Wallets = new HashSet<Wallet>();
        }

        public int IdUser { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
