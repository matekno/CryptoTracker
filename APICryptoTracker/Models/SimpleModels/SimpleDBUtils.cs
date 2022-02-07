namespace ApiCryptoTracker.Models.SimpleModels;

public class SimpleDBUtils
{
    public List<TokenXChain> GetTokensXChain(List<SimpleToken> tokens, List<SimpleChain> chains)
    {
        var tokensWithChain = tokens.Join(chains, token => token.FkChain, chain => chain.IdChain, (oToken, oChain) =>
        {
            // item1: idToken, item2: cgTicker, item3: chainName
            return new TokenXChain(oToken.IdToken, oToken.CgTicker, oChain.Name);
        }).ToList();
        return tokensWithChain;
    }

    public List<FinalBalance> GetFinalBalances(List<SimpleWalletXToken> walletXTokens, List<SimpleWallet> wallets, List<TokenXChain> tokensWithChain)
    {
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
            return new FinalBalance(oToken.Item2, oToken.Item1, oToken.Item3, oWallet.Address);
        }).ToList();
        return balances;

    }

}