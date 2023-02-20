//Configurations
enviromentVars env;
var nRegisters = 10000;
//var cacheSizes = new int[] { 500, 1000, 2000 }; //Aumentar é interessante...
var cacheSizes = new int[] { 7500, 5000, 2500 };
var CacheStrategies = new CacheStrategy[] { CacheStrategy.Fifo, CacheStrategy.LFU };
//var CacheStrategies = new CacheStrategy[] {CacheStrategy.LFU };
var nRequests = 1000000;

env = Utils.GetCommandLineArgs(args);
HttpClient client = Utils.ConfigureHttpClient(env);

AuthorizationClient serverClient = new AuthorizationClient(client);

Utils.TestCase(serverClient,nRegisters,nRequests,0,CacheStrategy.None);

Utils.TestCase(serverClient,nRegisters,nRequests,nRegisters,CacheStrategy.Only);

foreach (int size in cacheSizes)
    foreach (CacheStrategy strategy in CacheStrategies)
        Utils.TestCase(serverClient, nRegisters,nRequests,size,strategy);



