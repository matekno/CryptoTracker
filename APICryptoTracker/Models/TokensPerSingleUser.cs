using ApiCryptoTracker.Models.Interfaces;
using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models;

public class TokensPerSingleUser : IRequestListOfTokens
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    
    
    public List<ITokensPerSomething> Request()
    {
        return null;
    }

    public List<User> Users { get; set; }

    
    public TokensPerSingleUser(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils, List<User> users)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
        Users = users;
    }
    public List<ITokenWithOwner> Request(User user)
    {
        var toReturn = new List<ITokenWithOwner>();
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        foreach (var token in finalBalances)
        {
            var t = Utils.FindOwnerOfWallet(token.Address, Wallets, Users);
            if (t.IdUser == user.IdUser)
            {
                toReturn.Add(token);
            }
        }
        return toReturn;
    }
}