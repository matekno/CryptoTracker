using System.ComponentModel.Design;
using System.Linq;
using ApiCryptoTracker.Models;
using ApiCryptoTracker.Models.Interfaces;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public class FullTokensPerWallet2
{
    public List<Token> Tokens { get; }
    public List<Wallet> Wallets { get; }
    public List<WalletXToken> WalletXTokens { get; }
    public List<Chain> Chains { get; }
    public SimpleDBUtils Utils { get; }
    
    
    public FullTokensPerWallet2(CRIPTOSContext context, SimpleDBUtils utils)
    {
        Tokens = context.Tokens.ToList();
        Wallets = context.Wallets.ToList();
        WalletXTokens = context.WalletXTokens.ToList();
        Chains = context.Chains.ToList();
        Utils = utils;
    }
    
    public List<ITokensPerSomething> Request()
    {
        var listOfWalletsToReturn = new List<ITokensPerSomething>();
        var finalBalances = Utils.GetFinalBalances2(WalletXTokens, Wallets, Tokens, Chains);
        
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


