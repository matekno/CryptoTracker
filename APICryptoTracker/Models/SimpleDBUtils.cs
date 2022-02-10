using ApiCryptoTracker.Models.Interfaces;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCryptoTracker.Models;

public class SimpleDBUtils
{
    public List<ITokenWithOwner> GetFinalBalances(List<SimpleWalletXToken> walletXTokens, List<SimpleWallet> wallets, List<SimpleToken> tokens, List<SimpleChain> chains)
    {
        var list = new List<ITokenWithOwner>();
        
        var tokensWithChain = tokens.Join(chains, token => token.FkChain, chain => chain.IdChain, (oToken, oChain) =>
        {
            // item1: idToken, item2: cgTicker, item3: chainName
            return new TokenXChain(oToken.IdToken, oToken.CgTicker, oChain.Name);
        }).ToList();
        
        var balances = tokensWithChain.Join(walletXTokens, tokenC => tokenC.IdToken, wxt => wxt.FkToken, (oTokenC, oWxt) =>
        {
            // item1: cgTicker,
            // item2: balance,
            // item3: chain,
            // item4: fkWallet
            return new Tuple<string, double, string, int>(oTokenC.CgTicker, oWxt.TokenBalance, oTokenC.Chain, oWxt.FkWallet);
        }).Join(wallets, token => token.Item4, wallet => wallet.IdWallet, (oToken, oWallet) =>
        {
            // item1: balance,
            // item2: cgTicker,
            // item3: chain,
            // item4: address
            return new CompleteTokenWithBalance(){Balance = oToken.Item2, CgTicker = oToken.Item1, Chain = oToken.Item3, Address = oWallet.Address};
        }).ToList();
        list.AddRange(balances);
        return list;
    }
    public User FindOwnerOfWallet(string wallet, List<SimpleWallet> wallets, List<User> users)
    {
        var u = new User();

        var temp = (from w in wallets
                    join user in users on w.FkUser equals user.IdUser
                    where w.Address == wallet
                    select new User() {IdUser = user.IdUser, Email = user.Email, Name = user.Name, Password = user.Password}).FirstOrDefault();
        
        // return r;
        var stop = 0;
        return temp;
    }
    
    public List<ITokenWithOwner> GetFinalBalances2(List<WalletXToken> walletXTokens, List<Wallet> wallets, List<Token> tokens, List<Chain> chains)
    {
        var list = new List<ITokenWithOwner>();
        
        var tokensWithChain = tokens.Join(chains, token => token.FkChain, chain => chain.IdChain, (oToken, oChain) =>
        {
            return new TokenXChain2(oToken, oChain);
        }).ToList();


        foreach (var token in tokensWithChain)
        {
            foreach (var kvp in token.TokenXChain)
            {
                var balances = tokensWithChain.Join(walletXTokens, tokenC => kvp.Key.IdToken, wxt => wxt.FkToken, (oTokenC, oWxt) =>
                {
                    // item1: cgTicker,
                    // item2: balance,
                    // item3: chain,
                    // item4: fkWallet
                    return 
                    return new Tuple<string, double, string, int>(kvp.Key.CgTicker, oWxt.TokenBalance, oTokenC.Chain, oWxt.FkWallet);
                });
            }
        }
        
        // var balances = tokensWithChain.Join(walletXTokens, tokenC => tokenC., wxt => wxt.FkToken, (oTokenC, oWxt) =>
        // {
        //     // item1: cgTicker,
        //     // item2: balance,
        //     // item3: chain,
        //     // item4: fkWallet
        //     return new Tuple<string, double, string, int>(oTokenC.CgTicker, oWxt.TokenBalance, oTokenC.Chain, oWxt.FkWallet);
        // }).Join(wallets, token => token.Item4, wallet => wallet.IdWallet, (oToken, oWallet) =>
        {
            // item1: balance,
            // item2: cgTicker,
            // item3: chain,
            // item4: address
            return new CompleteTokenWithBalance(){Balance = oToken.Item2, CgTicker = oToken.Item1, Chain = oToken.Item3, Address = oWallet.Address};
        }).ToList();
        list.AddRange(balances);
        return list;
    }

}