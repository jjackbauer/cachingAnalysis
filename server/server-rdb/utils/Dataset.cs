using domain_core;

public static class Dataset
{
    public async static void PopulateDatabase(IBalanceRepository repository, long datasetSize)
    {   var rand = new Random();

        
        
        for (long c = (long) 1 ; c <= datasetSize; c++)
        {      
            await repository.Add(new AccountBalance
                {
                    UserID = c,
                    Amount = 10000*rand.NextDouble(),

                });
        }

        

        var rows =  await repository.Commit();

        Console.WriteLine($"Erased {rows} rows from database");

        return;
    }

    public async static void EraseDatabase(IBalanceRepository repository)
    {
        await repository.Erase();
        var rows =  await repository.Commit();

        Console.WriteLine($"Erased {rows} rows from database");
    }
}