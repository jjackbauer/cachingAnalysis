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

};

public struct enviromentVars
{
    public int Port { get; set; }
    public string Host { get; set; }
};