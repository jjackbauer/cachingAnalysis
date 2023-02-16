public static class Utils
{
    public static enviromentVars GetCommandLineArgs(string[] args)
    {
        enviromentVars env = new enviromentVars();

        if(args.Length > 2)
        {
            env.Host = args[1];
            env.Port = Int32.Parse(args[2]);
        }else
        {
            env.Host = "localhost";
            env.Port = 5122;
        }
        
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