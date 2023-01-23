using System.Text;

public enum CacheStrategy
{
    Fifo,
    LFU
}

public class Config{

    public readonly CacheStrategy _strategy;
    public  readonly long _size;

    public Config(CacheStrategy Strategy, long Size)
    {
        _strategy = Strategy;
        _size = Size;
    }

    public override string ToString(){
        var builder = new StringBuilder("Cache Config\n");

        builder.AppendFormat($"Cache Strategy: {(CacheStrategy) _strategy}\n");
        builder.AppendFormat($"Cache Size: {_size}\n");

        return builder.ToString();
    }
}