using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.Application.Services;

public interface IBookService
{
    Task AddAsync(AddBookRequestDto book);
    
    Task<BookResponseDto?> GetByIdAsync(Guid id);

    Task<List<BookResponseDto>?> GetAllAsync();
    
    Task<bool> UpdateAsync(UpdateBookRequestDto book);

    Task<bool> DeleteByIdAsync(Guid id);
}