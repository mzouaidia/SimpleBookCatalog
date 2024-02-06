using SimpleBookCatalog.Domain.Entities;

namespace SimpleBookCatalog.Application.Repositories;

public interface IBookRepository
{
    Task AddAsync(Book book);
    
    Task<Book?> GetByIdAsync(Guid id);

    Task<List<Book>> GetAllAsync();
    
    Task<bool> UpdateAsync(Book book);

    Task<bool> DeleteByIdAsync(Guid id);
}