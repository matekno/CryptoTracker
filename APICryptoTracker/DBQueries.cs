using System.ComponentModel.Design;
using System.Linq;
using ApiCryptoTracker.Models;
using ApiCryptoTracker.Models.SimpleModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCryptoTracker.TokensSegunWallet;

public class DBQueries
{
    /// <summary>
    /// Given a wallet address, returns a List with the balance of each token that the wallet posses, with proper chain.
    /// </summary>
    /// <param name="wallet">A valid string wallet address.</param>
    /// <returns>List of tokens</returns> 
    public List<TokensPerWallet> GetTokensPerWallet(string wallet)
    {
        using var context = new CRIPTOSContext();
        var tokens = context.Tokens.Select(token => new Tuple<int, string, int>(token.IdToken, token.CgTicker, token.FkChain)).ToList();
        var wallets = context.Wallets.Select(w => new Tuple<int, string, string>(w.IdWallet, w.Address, w.Nickname));
        var walletXTokens = context.WalletXTokens.Select(tmp => new Tuple<int, int, double>(tmp.FkToken, tmp.FkWallet, tmp.TokenBalance)).ToList();
        var chains = context.Chains.Select(chain => new Tuple<int, string>(chain.IdChain, chain.Name)).ToList();

        var tokensWithChain = tokens.Join(chains, token => token.Item3, chain => chain.Item1, (oToken, oChain) =>
        {
            // item1: idToken, item2: cgTicker, item3: chainName
            return new Tuple<int, string, string>(oToken.Item1, oToken.Item2, oChain.Item2);
        }).ToList();

        var balances = tokensWithChain.Join(walletXTokens, tokenC => tokenC.Item1, wxt => wxt.Item1, (oTokenC, oWxt) =>
        {
            // item1: cgTicker,
            // item2: balance,
            // item3: chain,
            // item4: fkWallet
            return new Tuple<string, double, string, int>(oTokenC.Item2, oWxt.Item3, oTokenC.Item3, oWxt.Item2);
        }).ToList();

        var finalBalances = balances.Join(wallets, token => token.Item4, wallet => wallet.Item1, (oToken, oWallet) =>
        {
            // item1: balance,
            // item2: cgTicker,
            // item3: chain,
            // item4: address
            return new Tuple<double, string, string, string>(oToken.Item2, oToken.Item1, oToken.Item3, oWallet.Item2);
        }).ToList();

        var result = (from token in finalBalances
                      where token.Item4 == wallet
                      group token by token.Item2
                      into g
                      select new TokensPerWallet
                      {
                          Token = g.First().Item2,
                          Balance = g.Sum(a => a.Item1),
                          Address = g.First().Item4,
                          Chain = g.First().Item3
                      }).ToList();

        return result;
        // SELECT TOKEN_BALANCE, TOKEN.CG_TICKER, CHAIN.NAME
        // FROM WALLET_X_TOKEN
        // JOIN WALLET ON WALLET_X_TOKEN.FK_WALLET = WALLET.ID_WALLET
        // JOIN TOKEN ON WALLET_X_TOKEN.FK_TOKEN = TOKEN.ID_TOKEN
        // JOIN CHAIN ON TOKEN.ID_TOKEN = CHAIN.ID_CHAIN
        // WHERE WALLET.ADDRESS = @wallet
    }
    public List<TokensPerWallet> GetTokensPerUser(int idUser)
    {
        using var context = new CRIPTOSContext();
        var tokens = context.Tokens.Select(token => new Tuple<int, string, int>(token.IdToken, token.CgTicker, token.FkChain)).ToList();
        var wallets = from wallet in context.Wallets
                       where wallet.FkUser == idUser
                       select new Tuple<int, string, string>(wallet.IdWallet, wallet.Address, wallet.Nickname);
        var walletXTokens = context.WalletXTokens.Select(tmp => new Tuple<int, int, double>(tmp.FkToken, tmp.FkWallet, tmp.TokenBalance)).ToList();
        var chains = context.Chains.Select(chain => new Tuple<int, string>(chain.IdChain, chain.Name)).ToList();

        var tokensWithChain = tokens.Join(chains, token => token.Item3, chain => chain.Item1, (oToken, oChain) =>
        {
            // item1: idToken, item2: cgTicker, item3: chainName
            return new Tuple<int, string, string>(oToken.Item1, oToken.Item2, oChain.Item2);
        }).ToList();

        var balances = tokensWithChain.Join(walletXTokens, tokenC => tokenC.Item1, wxt => wxt.Item1, (oTokenC, oWxt) =>
        {
            // item1: cgTicker,
            // item2: balance,
            // item3: chain,
            // item4: fkWallet
            return new Tuple<string, double, string, int>(oTokenC.Item2, oWxt.Item3, oTokenC.Item3, oWxt.Item2);
        }).ToList();

        var finalBalances = balances.Join(wallets, token => token.Item4, wallet => wallet.Item1, (oToken, oWallet) =>
        {
            // item1: balance,
            // item2: cgTicker,
            // item3: chain,
            // item4: address
            return new Tuple<double, string, string, string>(oToken.Item2, oToken.Item1, oToken.Item3, oWallet.Item2);
        }).ToList();

        var result = (from token in finalBalances
                      group token by token.Item2
                      into g
                      select new TokensPerWallet
                      {
                          Token = g.First().Item2,
                          Balance = g.Sum(a => a.Item1),
                          Address = g.First().Item4,
                          Chain = g.First().Item3
                      }).ToList();

        return result;
    }
}
