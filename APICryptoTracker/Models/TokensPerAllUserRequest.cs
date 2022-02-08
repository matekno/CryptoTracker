using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ApiCryptoTracker.Models;

public class TokensPerAllUserRequest
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    public List<User> Users { get; set; }
    
    public TokensPerAllUserRequest(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils, List<User> users)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
        Users = users;
    }

    public Dictionary<User, List<FinalBalance>> Request()
    {
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        var dict = new Dictionary<User, List<FinalBalance>>();

        foreach (var user in Users)
        {
            dict.Add(user, new List<FinalBalance>());
        }


        foreach (var keyValPair in dict)
        {
            foreach (var token in finalBalances)
            {
                var t = Utils.FindOwnerOfWallet(token.Address, Wallets, Users);
                if (t.IdUser == keyValPair.Key.IdUser)
                {
                    keyValPair.Value.Add(token);
                }
            }
        }

        return dict;
    }
    


}