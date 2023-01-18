using Microsoft.AspNetCore.Mvc;
using domain_core;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    [HttpPost(Name = "PostTransaction")]
    public PaymentAuthorizationResponse PostTransaction(){
        var a = DateTime.Now;
        var b = DateTime.Now;

       PaymentAuthorizationResponse output = new PaymentAuthorizationResponse{
            Approved = true,
            CacheHit = true,
            ArrivalTime = a,
            DepartureTime = b,
            TimeElapsedInNanosseconds = (b - a).Nanoseconds,
        };
        
        return output;
    }

    [HttpGet(Name = "It's alive")]
    public string ItsAlive(){
        return "I'm Alive";
    }
}