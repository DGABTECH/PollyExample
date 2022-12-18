using Client.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientPolicy _policy;


    public ClientController(ClientPolicy policy)
    {
        _policy = policy;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var client = new HttpClient();
        //var results = await client.GetAsync("http://localhost:5000/Server/75");

        var results = await _policy.WaitRetryPolicy.ExecuteAsync(() =>
            client.GetAsync("http://localhost:5000/Server/75")
        );
        
        if (results.IsSuccessStatusCode)
        {
            Console.WriteLine($"{DateTime.Now} --> Response is OK");
            return Ok();
        }
        
        Console.WriteLine($"{DateTime.Now} --> Response is Error");
        return Problem();
    }
}