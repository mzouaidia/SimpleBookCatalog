using Microsoft.AspNetCore.Components;
using SimpleBookCatalog.App.Interfaces;
using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.App.Pages;

public partial class BooksQuickGrid
{
    [Inject] private IBookService _service { get; set; }

    [Inject] private NavigationManager _nvm { get; set; }

    private IEnumerable<BookResponseDto>? _books { get; set; } = [];

    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var books = await _service.GetBooks();

        if (books?.Count > 0)
            _books = books;
    }

    private void EditBook(Guid id)
    {
        _nvm.NavigateTo($"book/qgd/{id}");
    }

    private async Task DeleteBook(Guid id)
    {
        var result = await _service.DeleteBook(id);

        if (result.Check)
        {
            _books = await _service.GetBooks();
        }

        Message = $"Coś poszło nie tak spróbuj ponownie!\n{result.Info}";
    }
}