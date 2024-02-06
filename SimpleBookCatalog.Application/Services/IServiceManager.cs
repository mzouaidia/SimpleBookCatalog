namespace SimpleBookCatalog.Application.Services;

public interface IServiceManager
{
    IBookService BookService { get; }
}