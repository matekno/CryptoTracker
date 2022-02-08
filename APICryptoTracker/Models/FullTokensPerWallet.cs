using System.ComponentModel.Design;
using System.Linq;
using ApiCryptoTracker.Models;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public class FullTokensPerWallet : IRequestListOfTokens
{
    public List<SimpleToken> Tokens { get; }
    public List<SimpleWallet> Wallets { get; }
    public List<SimpleWalletXToken> WalletXTokens { get; }
    public List<SimpleChain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    
    
    public FullTokensPerWallet(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
    }
    
    public List<ITokensPerSomething> Request()
    {
        var listOfWalletsToReturn = new List<ITokensPerSomething>();
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        
        var result = (from t in finalBalances
                     group t by t.Address 
                     into g 
                     select g).ToList();
        
        foreach (var wallet in result)
        {
            var final = new TokensPerWallet(){Address = wallet.Key};
            final.InitTokenList();
            foreach (var token in wallet)
            {
                final.Tokens.Add(token);
            }
            listOfWalletsToReturn.Add(final);
        }
        return listOfWalletsToReturn;
    }
}


