using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    [HttpPost(Name = "PostTransaction")]
    public string PostTransaction(){
        return "Rodou";
    }

    [HttpGet(Name = "It's alive")]
    public string ItsAlive(){
        return "I'm Alive";
    }
}