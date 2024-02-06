using Microsoft.AspNetCore.Components;
using SimpleBookCatalog.App.Interfaces;
using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.App.Pages;

public partial class BooksRadzenGrid
{
    [Inject] private IBookService _service { get; set; }

    [Inject] private NavigationManager _nvm { get; set; }

    private IEnumerable<BookResponseDto>? Books { get; set; } = [];

    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var books = await _service.GetBooks();

        if (books?.Count > 0)
            Books = books;
    }

    private void EditBook(Guid id)
    {
        _nvm.NavigateTo($"book/{id}");
    }

    private async Task DeleteBook(Guid id)
    {
        var result = await _service.DeleteBook(id);

        if (result.Check)
        {
            Books = await _service.GetBooks();
        }

        Message = $"Coś poszło nie tak spróbuj ponownie!\n{result.Info}";
    }
}