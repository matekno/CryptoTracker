using ApiCryptoTracker.Models.SimpleModels;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public interface ITokensRequest
{
    public List<FinalToken> Tokens { get; set; }
    public string Address { get; set; }
}