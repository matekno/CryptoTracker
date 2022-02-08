using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models;

public interface IRequestTokens
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }

    public List<ITokensStructureRequest> Request();


}