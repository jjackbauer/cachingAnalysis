public class Zipf
{
    private readonly long n;
    private readonly double h;
    private readonly Random r;
    private readonly double[] cdf;
    public Zipf(long N)
    {
        if(N <= 0 )
            throw new Exception("N must be greather than zero");

        n = N;

        h = Enumerable.Range(1, (int) N).Select(i => 1.0 / i).Sum();

        cdf = new double[N+1];

        cdf[0] = 0; 

        for(int c = 1 ; c <= N; c++)
            cdf[c] = cdf[c-1]+ 1.0 / (c*h);

        r = new Random(Guid.NewGuid().GetHashCode());
    }

    public long GetNext(){
        var u = r.NextDouble();
        int k = Array.BinarySearch(cdf, u);

        return k < 0 ? ~k:k; 
    }
}