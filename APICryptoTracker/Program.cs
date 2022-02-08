using ApiCryptoTracker.Models;
using Microsoft.EntityFrameworkCore;
using ApiCryptoTracker;
using ApiCryptoTracker.Models.SimpleModels;
using ApiCryptoTracker.TokensSegunWallet;
using ApiCryptoTracker.TokensSegunWallet.Models;

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CRIPTOSContext>(); // esto es inyeccion de dependencias. Inyectamos el contexto de la base de datos, y asi no tenemos que usar mas el using 
builder.Services.AddEndpointsApiExplorer(); // para usar swagger:
builder.Services.AddSwaggerGen(); // swagger
var app = builder.Build();
app.UseSwagger(); // swagger
app.UseSwaggerUI(); // swagger

#region nomehinchesloshuevos

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", (string name) => $"Hola {name}");
app.MapGet(
    "/hellowithname/{name}/{lastName}",
    (string name, string lastName) => $"Hola {name} {lastName}"
);

app.MapGet("/response", async () =>
{
    HttpClient client = new HttpClient(); // httpClient es una clase para hacer consultas http
    var response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos"); // hacemos una consulta asincrona a alguna api
    response.EnsureSuccessStatusCode(); // chequeamos que se haya hecho bien la consulta a la api
    string responseBody = await response.Content.ReadAsStringAsync(); // parsea la respuesta de la api a string
    return responseBody; // devuelve la respuesta en string como valor de la nueva api

});

#endregion

#region 22222222

app.MapGet("/tokens", () =>
{
    using var context = new CRIPTOSContext();
    return context.Tokens.ToList();
});

app.MapGet("/tokensInjected", async (CRIPTOSContext context) =>
    await context.Tokens.ToListAsync()
);

var temp = () =>
{
    return "hola";
};
app.MapGet("/tokensInjected2", temp);

app.MapPost("/post", async (Token token, CRIPTOSContext context) =>
{
    context.Add(token);
    await context.SaveChangesAsync();
    return $"{token.CgTicker}";
});



#endregion


// app.MapGet("/GetAllUserAssets/{idUser}", async (int idUser, CRIPTOSContext context) =>
// {
//     DBQueries Queries = new DBQueries();
//     return Queries.GetTokensPerUser(idUser);
// });

// var tokens = Queries.GetTokensPerWallet("0x74B0D20434FA140944f6074FF9E2B4E787faC1D5LL");

//TODO: en los metodos GetTokenPerUser y ¿GetTokenPerWallet? falta agrupar los tokens en caso de que se repitan. Hacer una funcion para los dos!!!!!!!
using (CRIPTOSContext context = new CRIPTOSContext())
{
    var tokens = context.Tokens.Select(token => new SimpleToken(token.IdToken, token.CgTicker, token.FkChain)).ToList();
    var wallets = context.Wallets.Select(w => new SimpleWallet(w.IdWallet, w.Address, w.Nickname, w.FkUser)).ToList();
    var walletXTokens = context.WalletXTokens.Select(tmp => new SimpleWalletXToken(tmp.FkToken, tmp.FkWallet, tmp.TokenBalance)).ToList();
    var chains = context.Chains.Select(chain => new SimpleChain(chain.IdChain, chain.Name)).ToList();
    // var users = context.Users.Select(u => new User()).ToList();
    var users = context.Users.ToList();
    var utils = new SimpleDBUtils();
    
    var GetTokensPerWallet = new FullTokensPerWallet(tokens, wallets, walletXTokens, chains, utils);
    var fullTokensPerWallet = GetTokensPerWallet.Request();

    var GetTokensPerUser = new FullTokensPerUserRequest(tokens, wallets, walletXTokens, chains, utils, users);
    var fullTokensPerUser = GetTokensPerUser.Request();
    
    var GetTokensPerSingleUser = new TokensPerSingleUser(tokens, wallets, walletXTokens, chains, utils, users); 
    var a = GetTokensPerSingleUser.Request(users[1]); // no anda bien
    

    var stop = 0;
}


app.Run();

