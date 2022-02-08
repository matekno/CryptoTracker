using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ApiCryptoTracker.Models;

public class FullTokensPerUserRequest
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    public List<User> Users { get; set; }
    
    public FullTokensPerUserRequest(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils, List<User> users)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
        Users = users;
    }

    public List<ITokensPerSomething> Request()
    {
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        var usersXTokensToReturn = new List<ITokensPerSomething>();
        


        foreach (var user in Users)
        {
            var tokensPerUser = new TokensPerUser();
            tokensPerUser.InitTokenList();
            tokensPerUser.User = user;
            foreach (var token in finalBalances)
            {
                var owner = Utils.FindOwnerOfWallet(token.Address, Wallets, Users);
                if (owner.IdUser == user.IdUser)
                {
                    tokensPerUser.Tokens.Add(token);
                    
                }
            }
            usersXTokensToReturn.Add(tokensPerUser);
        }
        return usersXTokensToReturn;
    }
    


}