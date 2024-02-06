using SimpleBookCatalog.Infrastructure.Context;
using SimpleBookCatalog.Application.Repositories;

namespace SimpleBookCatalog.Infrastructure.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly SimpleBookCatalogDbContext _context;
    
    public IBookRepository BookRepository { get; }
    
    public RepositoryWrapper(SimpleBookCatalogDbContext context)
    {
        _context = context;
        BookRepository = new BookRepository(_context);
    }
    
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(true, cancellationToken);
    }
}