using ApiCryptoTracker.Models.Interfaces;
using ApiCryptoTracker.TokensSegunWallet.Models;
namespace ApiCryptoTracker.Models.SimpleModels;

public class TokensPerWallet : ITokensPerSomething
{
    public List<ITokenWithOwner> Tokens { get; set; }
    public string Address { get; set; }

    public void InitTokenList()
    {
        Tokens = new List<ITokenWithOwner>();
    }
}

public class TokensPerUser : ITokensPerSomething
{
    public User User { get; set; }
    public List<ITokenWithOwner> Tokens { get; set; }
    public void InitTokenList()
    {
        Tokens = new List<ITokenWithOwner>();    }

}