namespace ApiCryptoTracker.Models.Interfaces;

public interface IToken
{
    public string CgTicker { get; init; }
}

public interface ITokenWithOwner : IToken
{
    public string Address { get; init; }
    public double Balance { get; init; }
}

public interface ITokensPerSomething
{
    public List<ITokenWithOwner> Tokens { get; set; }
    public void InitTokenList();
}
