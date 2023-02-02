namespace domain_core;

public struct Balance
{
    public long UserID { get; set; }
    public double Amount { get; set; }
}

public class AccountBalance :Imodel
{
    public AccountBalance()
    {
        
    }
    public long UserID { get; set; }
    public double Amount { get; set; }

    public long GetId()
    {
        return UserID;
    }

    public Imodel New()
    {
        return new AccountBalance();
    }
}
public struct PaymentAuthorizationRequest{
    public long UserID { get; set; }
    public double Value { get; set; }
}
public struct PaymentAuthorizationResponse{
    public bool Approved { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public double TimeElapsedInNanosseconds {get; set;}
    public bool CacheHit {get; set;}
}