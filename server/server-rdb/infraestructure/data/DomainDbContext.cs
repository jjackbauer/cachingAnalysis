using domain_core;
using Microsoft.EntityFrameworkCore;

public class DomainDbContext : DbContext
{
    public  required DbSet<AccountBalance> AccountBalances {get; set;}
    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new BalanceMapping());

        base.OnModelCreating(modelBuilder);
    }

  
}  

public static class DbContextExtensions
{
    public static void Clear<T>(this DbContext context) where T : class
    {
        context.Set<T>().RemoveRange(context.Set<T>());
        context.SaveChanges();
    }
}