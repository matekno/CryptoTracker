namespace ApiCryptoTracker.Models.SimpleModels;

public class SimpleWalletXToken
{
    public int FkToken { get; }
    public int FkWallet { get; }
    public double TokenBalance { get; }

    public SimpleWalletXToken(int fkToken, int fkWallet, double tokenBalance)
    {
        FkToken = fkToken;
        FkWallet = fkWallet;
        TokenBalance = tokenBalance;
    }
}

public class SimpleChain
{

    public int IdChain { get; }
    public string Name { get; }

    public SimpleChain(int id, string name)
    {
        IdChain = id;
        Name = name;
    }
}

public class SimpleToken
{
    public int IdToken { get; }
    public string CgTicker { get; }
    public int FkChain { get; }

    public SimpleToken(int id, string cg, int fkChain)
    {
        IdToken = id;
        CgTicker = cg;
        FkChain = fkChain;
    }
    
}

public class SimpleWallet
{
    public  int IdWallet;
    public string Address;
    public string Nickname;

    public SimpleWallet(int id, string address, string nickname)
    {
        IdWallet = id; 
        Address = address;
        Nickname = nickname;
    }
}
