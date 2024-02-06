namespace SimpleBookCatalog.Application.Repositories;

public interface IRepositoryWrapper
{
    IBookRepository BookRepository { get; }
    
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}