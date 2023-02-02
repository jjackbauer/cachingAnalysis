using domain_core;
using Microsoft.EntityFrameworkCore;

public class BalanceRepository : IBalanceRepository
{
    private readonly DomainDbContext _context;

    public BalanceRepository(DomainDbContext context)
    {
        _context = context;
    }

    public async Task Add(AccountBalance input)
    {
        await _context.AddAsync(input);
        
        return;
    }

    public async Task AddMany(IEnumerable<AccountBalance> input)
    {
        await _context.AddRangeAsync(input.ToArray());

        return;
    }

    public async Task<long> Commit()
    {
       return await _context.SaveChangesAsync();
    }

    public async Task<AccountBalance?> Get(long id)
    {
       return await _context.AccountBalances.FirstOrDefaultAsync( b => b.UserID == id);
    }
}