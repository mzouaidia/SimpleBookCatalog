using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Domain.Entities;

namespace SimpleBookCatalog.Infrastructure.Context;

public class SimpleBookCatalogDbContext(DbContextOptions<SimpleBookCatalogDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(cf => cf.MigrationsHistoryTable("__EFMigrationsHistory", "sbc"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(SimpleBookCatalogDbContext).Assembly);
        builder.HasDefaultSchema("sbc");

        builder.Entity<Book>().HasIndex(f => f.Title).IsUnique();
        builder.Entity<Book>().Property(e => e.Id).HasDefaultValueSql("newid()");
        builder.Entity<Book>().Property(e => e.Title).IsRequired().HasMaxLength(100);
        builder.Entity<Book>().Property(e => e.Author).IsRequired().HasMaxLength(100);
        builder.Entity<Book>().Property(e => e.PublicationDate).IsRequired();
    }
}