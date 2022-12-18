using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ServerController : ControllerBase
{

    [HttpGet("{i:int}")]
    public IActionResult Get([FromRoute]int i)
    {
        var rand = new Random();
        var r = rand.Next(1, 101);
        if (i < r)
        {
            Console.WriteLine($"{DateTime.Now} --> Server OK");
            return Ok();
        }
        Console.WriteLine($"{DateTime.Now} --> Server Error");
        return Problem();
    }
}