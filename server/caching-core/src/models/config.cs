using System.Text;

public enum CacheStrategy
{
    Fifo,
    LFU,
    None,
    Only
}

public class Config{

    public  CacheStrategy _strategy {get; set;}
    public  long _size {get; set;}

    public Config(CacheStrategy Strategy, long Size)
    {
        _strategy = Strategy;
        _size = Size;
    }

    public override string ToString(){
        var builder = new StringBuilder("\nCache Config\n");

        builder.AppendFormat($"Cache Strategy: {(CacheStrategy) _strategy}\n");
        builder.AppendFormat($"Cache Size: {_size}\n");

        return builder.ToString();
    }

    public void Configure(CacheStrategy Strategy, long Size){
        _size = Size;
        _strategy = Strategy;
    }
}