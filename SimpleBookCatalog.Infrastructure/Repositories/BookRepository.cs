using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Domain.Entities;
using SimpleBookCatalog.Infrastructure.Context;
using SimpleBookCatalog.Application.Repositories;

namespace SimpleBookCatalog.Infrastructure.Repositories;

public class BookRepository(SimpleBookCatalogDbContext context) : IBookRepository
{
    public async Task AddAsync(Book book) => await context.Books.AddAsync(book);
    
    public async Task<Book?> GetByIdAsync(Guid id) => await context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Book>> GetAllAsync() => await context.Books.AsNoTracking().ToListAsync();

    public async Task<bool> UpdateAsync(Book book)
    {
        context.Entry(book).State = EntityState.Modified;
       
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var result = await context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (result is null) return false;
        
        var toDelete = context.Books.Remove(result);
        
        return toDelete.State == EntityState.Deleted;
    }
}
