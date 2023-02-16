using System.Text;
using domain_core;
using Newtonsoft.Json;

//Configurations
enviromentVars env;
var nRegisters = 10000;
var cacheSizes = new int[] {500, 1000, 2000};
var CacheStrategies =  new CacheStrategy[] {CacheStrategy.Fifo, CacheStrategy.LFU };
var nRequests = 1000000;

var z = new Zipf(nRegisters);

env  = Utils.GetCommandLineArgs(args);
HttpClient client = Utils.ConfigureHttpClient(env);

AuthorizationClient serverClient = new AuthorizationClient(client);

foreach (int size in cacheSizes)
    foreach(CacheStrategy strategy in CacheStrategies)
    {
        while(!serverClient.ConfigureServer(size, strategy));

        var Config = serverClient.GetServerConfiguration();

        if(Config is not null)
        {
            var success = Config._size == size && Config._strategy == strategy;

            Console.WriteLine($"Running Server Configurations | {Config.ToString()}");
            Console.WriteLine($"Configuration Process Success | {success}\n");   
        }

        var responses = new List<PaymentAuthorizationResponse>(nRequests);
        for(int c =0 ; c < nRequests ; c++)
        { 
            PaymentAuthorizationRequest req = new PaymentAuthorizationRequest(){
                UserID = z.GetNext(),
                Value = 20
            };
            Console.Write($"{req.UserID} ");
            //var res = serverClient.AuthorizePayment(req);
            ;
        }
    }



