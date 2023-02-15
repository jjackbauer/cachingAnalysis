using System.Text;
using domain_core;
using Newtonsoft.Json;

var host = "localhost";
var port = 5122;

GetCommandLineArgs();

HttpClient client = new HttpClient{
    BaseAddress = new Uri($"http://{host}:{port}")
};


ConfigureHttpClient();


ConfigureServer(3, CacheStrategy.Fifo);//Temporário







void GetCommandLineArgs()
{
    if(args.Length > 2)
    {
        host = args[1];
        port = Int32.Parse(args[2]);
    }
        
}




void ConfigureHttpClient()
{
    var builder = new System.UriBuilder{
        Host = host,
        Port = port,
    };

    client.BaseAddress = builder.Uri;

}

void ConfigureServer(long cacheSize, CacheStrategy strategy)
{
    var route = $"api/v1/Authorization";
    ServerConfigurationRequest req = new ServerConfigurationRequest{
        cacheSize = cacheSize,
        strategy = strategy
    };

    StringContent content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8,"application/json");
    var res = client.PutAsync(route, content).GetAwaiter().GetResult();

    if (!res.IsSuccessStatusCode)
    {
        Console.WriteLine($"error configuring server | cacheSize: {cacheSize} strategy: {(CacheStrategy) strategy}");
        return;
    }
    var stringRes = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

    Config ?output = JsonConvert.DeserializeObject<Config>(stringRes);

    if (output is null)
    {
        Console.WriteLine($"error parsing response from  server | response {stringRes}");
        return;
    }

    Console.WriteLine($"server sucessfully configured | cacheSize: {output._size} strategy: {(CacheStrategy) output._strategy}");
}