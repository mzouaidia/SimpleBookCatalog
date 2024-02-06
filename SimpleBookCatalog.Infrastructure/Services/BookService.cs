using Mapster;
using SimpleBookCatalog.Domain.Entities;
using SimpleBookCatalog.Application.Dtos;
using SimpleBookCatalog.Application.Services;
using SimpleBookCatalog.Application.Repositories;

namespace SimpleBookCatalog.Infrastructure.Services;

public class BookService(IRepositoryWrapper repo) : IBookService
{
    public async Task AddAsync(AddBookRequestDto book)
    {
        await repo.BookRepository.AddAsync(book.Adapt<Book>());
        await repo.SaveAsync();
    }

    public async Task<BookResponseDto?> GetByIdAsync(Guid id)
    {
        var result = await repo.BookRepository.GetByIdAsync(id);

        return result.Adapt<BookResponseDto>();
    }

    public async Task<List<BookResponseDto>?> GetAllAsync()
    {
        var result = await repo.BookRepository.GetAllAsync();
        
        return result.Adapt<List<BookResponseDto>>();
    }

    public async Task<bool> UpdateAsync(UpdateBookRequestDto book)
    {
        var updateBook = await repo.BookRepository.GetByIdAsync(book.Id);
        
        if (updateBook is null) return false;
        
        await repo.BookRepository.UpdateAsync(book.Adapt<Book>());
        var result = await repo.SaveAsync();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        await repo.BookRepository.DeleteByIdAsync(id);

        var result = await repo.SaveAsync();

        return result > 0;
    }
}