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
}