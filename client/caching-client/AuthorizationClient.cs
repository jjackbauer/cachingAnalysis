using System.Text;
using domain_core;
using Newtonsoft.Json;

public class AuthorizationClient
{
    private readonly HttpClient _client;
    private static string _route = $"api/v1/Authorization";

    public AuthorizationClient(HttpClient client)
    {
        _client = client;
    }

    public bool ConfigureServer(long cacheSize, CacheStrategy strategy)
    {
        ServerConfigurationRequest req = new ServerConfigurationRequest{
            cacheSize = cacheSize,
            strategy = strategy
        };

        StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8,"application/json");
        var res = _client.PutAsync(_route, content).GetAwaiter().GetResult();

        if (!res.IsSuccessStatusCode)
        {
            Console.WriteLine($"error configuring server | cacheSize: {cacheSize} strategy: {(CacheStrategy) strategy}");
            return false;
        }
        var stringRes = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        Config ?output = JsonConvert.DeserializeObject<Config>(stringRes);

        if (output is null)
        {
            Console.WriteLine($"error parsing response from  server | response {stringRes}");
            return false;
        }

        Console.WriteLine($"server sucessfully configured | cacheSize: {output._size} strategy: {(CacheStrategy) output._strategy}");
        return true;
    }  
    public Config? GetServerConfiguration()
    {
       var res = _client.GetAsync(_route, CancellationToken.None).GetAwaiter().GetResult();

       if (!res.IsSuccessStatusCode)
        {
            Console.WriteLine($"error getting server configuration");
            return null;
        }

        var stringRes = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

       return JsonConvert.DeserializeObject<Config>(stringRes);
    }

    public PaymentAuthorizationResponse? AuthorizePayment(PaymentAuthorizationRequest input)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8,"application/json");
        var res = _client.PostAsync(_route, content).GetAwaiter().GetResult();

        if (!res.IsSuccessStatusCode)
        {
            Console.WriteLine($"error authorizing request | {input}");
            return null;
        }

        var stringRes = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();


        return JsonConvert.DeserializeObject<PaymentAuthorizationResponse>(stringRes);
    } 
}