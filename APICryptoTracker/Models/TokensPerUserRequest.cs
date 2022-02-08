using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ApiCryptoTracker.Models;

public class TokensPerUserRequest : IRequestTokens
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

    public TokensPerUserRequest(List<SimpleToken> tokens, List<SimpleWallet> wallets, List<SimpleWalletXToken> walletXTokens, List<SimpleChain> chains, SimpleDBUtils utils)
    {
        Tokens = tokens;
        Wallets = wallets;
        WalletXTokens = walletXTokens;
        Chains = chains;
        Utils = utils;
    }

    public List<ITokensStructureRequest> Request(List<User> users)
    {
        var toReturn = new List<ITokensStructureRequest>();
        var finalBalances = Utils.GetFinalBalances(WalletXTokens, Wallets, Tokens, Chains);
        foreach (var token in finalBalances)
        {
            // pregunta: yo aca tengo que inyectar users.
            // eso no me convierte en dependiente? si tuviera que cambiar algo es un quilombo barbaro
            // ademas, siento que hay algo que falla, porque no puede ser que cada vez que agrego una dependencia tengo que hacer a whole lio en el constructor
            // y por otro lado, siento que tengo mucho mas quilombo del que deberia.
            
            // otra cosa que me pasa es la siguiente
            // GetFinalBalances trae n cosas, pero yo ahora necesito que traiga esas n + 1. Como hago sin hacer copy paste?
            // me imagino que podria segregar mas la responsabilidad, y que despues herede algo, pero no me parece comodo.

            var user = FindOwnerOfWallet(token.Address, Wallets, users);
            

        }
        


        return toReturn;
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
}