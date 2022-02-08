using System.ComponentModel.Design;
using System.Linq;
using ApiCryptoTracker.Models;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public class TokensPerWalletRequest
{
    private List<SimpleToken> _tokens;
    private List<SimpleWallet> _wallets;
    private List<SimpleWalletXToken> _walletXTokens;
    private List<SimpleChain> _chains;
    private SimpleDBUtils _utils;
    
    
    public TokensPerWalletRequest(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils)
    {
        _tokens = tokens;
        _wallets = wallets;
        _walletXTokens = walletXTokens;
        _chains = chains;
        _utils = utils;

    }
    
    public List<ITokensRequest> GetTokensPerWallet()
    {
        var listOfWalletsToReturn = new List<ITokensRequest>();
        var finalBalances = _utils.GetFinalBalances(_walletXTokens, _wallets, _tokens, _chains);
        
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
                var t = new FinalToken() {Balance = token.Balance, CgTicker = token.CgTicker};
                final.Tokens.Add(t);
            }
            listOfWalletsToReturn.Add(final);
        }
        return listOfWalletsToReturn;
    }
}


