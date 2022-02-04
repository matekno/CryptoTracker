using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WEBCryptoTracker.Models;

namespace WEBCryptoTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public int IdUser = 2;

    public IActionResult Index()
    {
        // history = GetWalletHistory() = una funcion que consulta y parsea en la api el historial, para cargarlo en el grafico
        // LoadHistoryGraph(history) = una funcion que con la historia del portfolio, rellena el grafico de linea
        var ans = CallApiTest();
        // assets = GetCompleteWalletAssets() = una funcion que consulta y parsea en la api, y trae todos los tokens, y balances del usuario
        // LoadInTable(int n, assets) = una funcion que recibe un json o un objeto? y una cantidad de columnas y lo carga en una tabla de html  
        // LoadPieChart(assets) = una funcion que con los assets del portfolio, rellena el grafico circular 
        return View();
        
    }

    public IActionResult WalletVisualizer()
    {
        // wallets = GetWallets() = una funcion que recibe y parsea todas las wallets del usuario. Debe devolver, ademas, las chains que usa, y el capital total
        // LoadInTable(int n, assets) = una funcion que recibe un json o un objeto? y una cantidad de columnas y lo carga en una tabla de html  
        // LoadPieChart(wallets) = una funcion que con las wallets, rellena el grafico circular 
        
        // si se hace aca, habria que poner las funciones de formulario para hacer una nueva wallet
        // si permitimos que se registre una wallet con web3, tambien estaria bueno que este aca.

        return View();
    }

    #region Create-Token-Region

    public async Task<string> CallApiTest()
    {
        HttpClient client = new HttpClient(); // httpClient es una clase para hacer consultas http
        var response = await client.GetAsync($"https://localhost:7034/GetAllUserAssets/{IdUser}"); // hacemos una consulta asincrona a alguna api
        response.EnsureSuccessStatusCode(); // chequeamos que se haya hecho bien la consulta a la api
        string responseBody = await response.Content.ReadAsStringAsync(); // parsea la respuesta de la api a string
        return responseBody;
    }
    public IActionResult CreateToken()
    {
        // 
        return View();
    }
    
    [HttpPost]
    public ActionResult CreateTokenForm(string token, bool isNative, string chain = null, string contract = null)
    {
        // code = PostToken() = chequea que no exista el token. si existe devuelve un codigo de error, y sino, devuelve un 200.


        
    return View("Success");
    }
    #endregion


    #region Create-Chain-Region
    
    public IActionResult CreateChain()
    {
        return View();
    }
    public ActionResult CreateChainForm(string chain, string rpc, int id, string blockExplorer)
    {
        // code = PostChain = valida que la chain exista. caso contrario, la crea en la base de datos.
        // TODO estaria bueno que despues de crear la chain, o de crear el token, te ofrezca hacer una transaccion con el mismo.
        return View("Success");
    }
    

    #endregion


    #region Add-Transaction-Region

    public IActionResult AddTransaction()
    {
        // 
        return View();
    }
    public ActionResult AddTransactionForm(string token, string wallet, bool bought, int operation)
    {
        // PostTransaction() = un metodo que publica la transaccion a la base de datos
        
        return View("Success");
    }

    #endregion

    #region Modify-Balance-Region

    public IActionResult ModifyBalance()
    {
        return View();
    }
    public ActionResult ModifyBalanceForm(string token, int newBalance)
    {
        //GetActualBalance TODO: estaria bueno que con javascript, apenas el usuario introduzca el token a modificar, le salga una card al lado, con los detalles actuales del token
        // PostNewBalance() = guarda en la base de datos el nuevo balance
        return View("Success");
    }

    #endregion
    

    public ActionResult Success()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}
