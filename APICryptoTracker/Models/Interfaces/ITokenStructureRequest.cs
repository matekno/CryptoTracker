using ApiCryptoTracker.Models.SimpleModels;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public interface ITokensStructureRequest
{
    public List<FinalToken> Tokens { get; set; }

    public void InitTokenList();
    // public string Address { get; set; }
}