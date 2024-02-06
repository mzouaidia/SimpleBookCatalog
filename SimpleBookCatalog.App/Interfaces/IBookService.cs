using SimpleBookCatalog.App.Models;
using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.App.Interfaces;

public interface IBookService
{
    Task<List<BookResponseDto>?> GetBooks();
    
    Task<BookResponseDto?> GetBookById(Guid id);

    Task<ServiceResult> AddBook(AddBookRequestDto book);

    Task<ServiceResult> UpdateBook(UpdateBookRequestDto book);

    Task<ServiceResult> DeleteBook(Guid id);
}