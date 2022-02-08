using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models.SimpleModels;

public class TokensStructurePerWallet : ITokensStructureRequest
{
    public List<FinalToken> Tokens { get; set; }
    public string Address { get; set; }

    public void InitTokenList()
    {
        Tokens = new List<FinalToken>();
    }

}

public class TokensStructurePerUser : ITokensStructureRequest
{
    public User User { get; set; }
    public List<FinalToken> Tokens { get; set; }
    public void InitTokenList()
    {
        Tokens = new List<FinalToken>();
    }

}