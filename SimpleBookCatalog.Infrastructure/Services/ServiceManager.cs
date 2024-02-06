using SimpleBookCatalog.Application.Services;
using SimpleBookCatalog.Application.Repositories;

namespace SimpleBookCatalog.Infrastructure.Services;

public class ServiceManager(IRepositoryWrapper repo) : IServiceManager
{
    public IBookService BookService { get; } = new BookService(repo);
}