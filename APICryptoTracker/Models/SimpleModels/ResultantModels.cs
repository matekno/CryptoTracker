using ApiCryptoTracker.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiCryptoTracker.Models.SimpleModels;

public class TokenXChain
{
    public int IdToken { get; }
    public string CgTicker { get; }
    public string Chain { get; }

    public TokenXChain(int id, string cg, string chain)
    {
        IdToken = id;
        CgTicker = cg;
        Chain = chain;
    }
}

public class CompleteTokenWithBalance : ITokenWithOwner
{
    public double Balance { get; init; }
    public string CgTicker { get; init; }
    public string Chain { get; init; }
    public string Address { get; init; }

    // public CompleteTokenWithBalance(double balance, string cgTicker, string chain, string address)
    // {
    //     Balance = balance;
    //     CgTicker = cgTicker;
    //     Chain = chain;
    //     Address = address;
    // }
}


public class TokenWithBalance : IToken
{    
    public string CgTicker { get; init; }
    public double Balance { get; init; }
}

// public class UserXTokens
// {
//     public string User;
//     public List<FinalToken> tokens;
// }
//
// public class UserXWallet
// {
//     public new Dictionary<User, Wallet> Dictionary;
// }