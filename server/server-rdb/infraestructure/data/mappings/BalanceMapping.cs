
using domain_core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class BalanceMapping : IEntityTypeConfiguration<AccountBalance>
{
    public void Configure(EntityTypeBuilder<AccountBalance> builder)
    {
        builder.ToTable("tb_acc_balance");
        builder.HasKey(p => p.UserID);
        builder.Property(p => p.Amount);
    }
}