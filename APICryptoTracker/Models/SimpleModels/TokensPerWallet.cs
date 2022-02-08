using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models.SimpleModels;

public class TokensPerWallet : ITokensRequest
{
    public List<FinalToken> Tokens { get; set; }
    public string Address { get; set; }

    public void InitTokenList()
    {
        Tokens = new List<FinalToken>();
    }

}

public class TokensPerUser : ITokensRequest
{
    public List<FinalToken> Tokens { get; set; }
    public string Address { get; set; }
    public void InitTokenList()
    {
        Tokens = new List<FinalToken>();
    }

}