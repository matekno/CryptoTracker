using System.ComponentModel.Design;
using System.Linq;
using ApiCryptoTracker.Models;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCryptoTracker.TokensSegunWallet.Models;

public class TokensPerUserRequest
{
    private List<SimpleToken> _tokens;
    private List<SimpleWallet> _wallets;
    private List<SimpleWalletXToken> _walletXTokens;
    private List<SimpleChain> _chains;
    private SimpleDBUtils _utils;
    
    
    public TokensPerUserRequest(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils)
    {
        _tokens = tokens;
        _wallets = wallets;
        _walletXTokens = walletXTokens;
        _chains = chains;
        _utils = utils;

    }
    
    public void GetTokensPerUser()
    {
        var tokensWithChain = _utils.GetTokensXChain(_tokens, _chains);
        var finalBalances = _utils.GetFinalBalances(_walletXTokens, _wallets, tokensWithChain);
        
        var result = (from token in finalBalances
                      group token by token.Address
                      into g
                      select new TokensPerWallet
                      {
                          Token = g.First().CgTicker,
                          Balance = g.Sum(a => a.Balance),
                          Address = g.First().Address,
                          Chain = g.First().Chain
                      }).ToList();
        
        
        // List<ITokenRequest> final = (ITokenRequest)result.ToList();

    }

}


