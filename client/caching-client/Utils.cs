using domain_core;

public static class Utils
{
    public static enviromentVars GetCommandLineArgs(string[] args)
    {
        enviromentVars env = new enviromentVars();

        /*Console.WriteLine($"args.Length:{args.Length}");
        foreach (string arg in args)
            Console.WriteLine($"args[]: {arg}");*/

        if(args.Length == 2)
        {
            env.Host = args[0];
            env.Port = Int32.Parse(args[1]);

            //Console.WriteLine("Command Line Configs");
        }else
        {
            env.Host = "localhost";
            env.Port = 5000;

            //Console.WriteLine("Default Configs");
        }

        Console.WriteLine($"Host:{env.Host} Port:{env.Port}");
        return env;
    }
    public static HttpClient ConfigureHttpClient(enviromentVars env)
    {
        HttpClient client = new HttpClient{
            BaseAddress = new Uri($"http://{env.Host}:{env.Port}")
        };

        return client;
    }

    public static void TestCase(AuthorizationClient serverClient,int nRegisters, int nRequests, int cacheSize, CacheStrategy strategy)
    {
        var z = new Zipf(nRegisters);
        var rGen = new RequestGenerator(z);

         while (!serverClient.ConfigureServer(cacheSize, strategy)) ;

        var Config = serverClient.GetServerConfiguration();

        if (Config is not null)
        {
            var success = Config._size == cacheSize && Config._strategy == strategy;

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

        var w = new CsvWriter<PaymentAuthorizationResponse>($"exp/csv/{strategy}-{cacheSize}-{nRequests}-{DateTime.Now.Ticks}.csv");
        w.WriteListToCsv(responses);

    }

};

public struct enviromentVars
{
    public int Port { get; set; }
    public string Host { get; set; }
};