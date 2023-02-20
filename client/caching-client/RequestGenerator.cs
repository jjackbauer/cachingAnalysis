using domain_core;

public class RequestGenerator
{
    private readonly Zipf _z;
    private readonly Random _rand;

    public RequestGenerator(Zipf z)
    {
        _z = z;
        _rand = new Random(Guid.NewGuid().GetHashCode());
    }

    public PaymentAuthorizationRequest GetNext()
    {
        return new PaymentAuthorizationRequest{
            UserID = _z.GetNext(),
            Value = 1000*_rand.NextDouble()
        };
    }
};