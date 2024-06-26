using Srv_Sale.Models;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace Srv_Sale.Data;

public class SaleDbContext : DbContext
{
    public SaleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}