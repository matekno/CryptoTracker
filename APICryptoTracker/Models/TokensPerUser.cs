using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;

namespace ApiCryptoTracker.Models;

public class TokensPerUser : IRequestListOfTokens
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    public List<ITokensStructureRequest> Request()
    {
        throw new NotImplementedException();
    }

    public List<User> Users { get; set; }

    
    public TokensPerUser(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils, List<User> users)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
        Users = users;
    }
    public List<ITokensStructureRequest> Request(User user)
    {
        var toReturn = new List<FinalBalance>();
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        
        foreach (var u in Users)
        {
            foreach (var token in finalBalances)
            {
                var t = Utils.3(token.Address, Wallets, Users);
                if (t.IdUser == user.IdUser)
                {
                    toReturn.Add(token);
                }
            }
        }


        return null;
    }
}