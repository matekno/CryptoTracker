using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models.SimpleModels;

public class TokensPerWallet : ITokensRequest
{
    public double Balance { get; set; }
    public List<SimpleToken> Tokens { get; set; }
    public string Token { get; set; }
    public string Chain { get; set; }
    public string Address { get; set; }

}

public class TokensPerUser : ITokensRequest
{
    public double Balance { get; set; }
    public List<SimpleToken> Tokens { get; set; }
    public string Token { get; set; }
    public string Chain { get; set; }
    public string Address { get; set; }

}