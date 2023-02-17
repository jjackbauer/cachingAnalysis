using System.Text;
using domain_core;
using Newtonsoft.Json;

//Configurations
enviromentVars env;
var nRegisters = 10000;
//var cacheSizes = new int[] { 500, 1000, 2000 }; //Aumentar é interessante...
var cacheSizes = new int[] { 5000, 4000, 3000 };
var CacheStrategies = new CacheStrategy[] { CacheStrategy.Fifo, CacheStrategy.LFU };
var nRequests = 1000000;

var z = new Zipf(nRegisters);
var rGen = new RequestGenerator(z);

env = Utils.GetCommandLineArgs(args);
HttpClient client = Utils.ConfigureHttpClient(env);

AuthorizationClient serverClient = new AuthorizationClient(client);

foreach (int size in cacheSizes)
    foreach (CacheStrategy strategy in CacheStrategies)
    {
        while (!serverClient.ConfigureServer(size, strategy)) ;

        var Config = serverClient.GetServerConfiguration();

        if (Config is not null)
        {
            var success = Config._size == size && Config._strategy == strategy;

            Console.WriteLine($"Running Server Configurations | {Config.ToString()}");
            Console.WriteLine($"Configuration Process Success | {success}\n");
        }

        var UserIDs = new List<long>(nRequests);
        var Values = new List<double>(nRequests);
        var ServiceTimes = new List<double>(nRequests);
        var ServiceTimesDb = new List<double>(nRequests);
        var ServiceTimesCache = new List<double>(nRequests);
        var responses = new List<PaymentAuthorizationResponse>(nRequests);

        for (int c = 0; c < nRequests; c++)
        {
            
            PaymentAuthorizationRequest req = rGen.GetNext();
            UserIDs.Add(req.UserID);
            Values.Add(req.Value);
            
            var res = serverClient.AuthorizePayment(req);

            if(res is not null){

                responses.Add(res.Value);
                ServiceTimes.Add(res.Value.TimeElapsedInNanosseconds);

                if(res.Value.CacheHit){
                    ServiceTimesCache.Add(res.Value.TimeElapsedInNanosseconds);
                }else{
                    ServiceTimesDb.Add(res.Value.TimeElapsedInNanosseconds);
                }
            }

            if(c % 10000 == 0){
                Console.WriteLine($"{c}/{nRequests} - Done | Hr {(double)ServiceTimesCache.Count/(c+1)}");
            }
        }

        Console.WriteLine($"Requests - Done | Hr {(double)ServiceTimesCache.Count/(nRequests)}");

        Statistics.Stats<long>("UserIds Stats",UserIDs);
        Statistics.Stats<double>("Values Stats", Values);
        Statistics.Stats<double>("Ts (all)", ServiceTimes);
        Statistics.Stats<double>("Ts (cache)", ServiceTimesCache);
        Statistics.Stats<double>("Ts (db)", ServiceTimesDb);

        var w = new CsvWriter<PaymentAuthorizationResponse>($"exp/csv/{strategy}-{size}-{nRequests}-{DateTime.Now.Ticks}.csv");
        w.WriteListToCsv(responses);
    }



