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

public class TokenXChain2
{
    public Dictionary<Token, Chain> TokenXChain { get; init; }

    public TokenXChain2(Token token, Chain chain)
    {
        TokenXChain = new Dictionary<Token, Chain>();
        TokenXChain.Add(token,chain);
    }
}

public class CompleteTokenWithBalance : ITokenWithOwner
{
    public double Balance { get; init; }
    public string CgTicker { get; init; }
    public string Chain { get; init; }
    public string Address { get; init; }
    
}


public class TokenWithBalance : IToken
{    
    public string CgTicker { get; init; }
    public double Balance { get; init; }
}
