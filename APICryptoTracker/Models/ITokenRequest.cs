using ApiCryptoTracker.Models.SimpleModels;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public interface ITokensRequest
{
    public double Balance { get; set; }
    public List<SimpleToken> Tokens { get; set; } 
    public string Token { get; set; }
    public string Chain { get; set; }
    public string Address { get; set; }
}