using ApiCryptoTracker.Models.Interfaces;
using ApiCryptoTracker.Models.SimpleModels;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public interface ITokensPerSomething
{
    public List<ITokenWithOwner> Tokens { get; set; }
    public void InitTokenList();
}

