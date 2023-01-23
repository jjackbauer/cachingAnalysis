using Microsoft.EntityFrameworkCore;

public class DomainDbContext : DbContext{
    public DbSet<AccountBalance> AccountBalances {get; set;}
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new BalanceMapping());

        base.OnModelCreating(modelBuilder);
    }
}