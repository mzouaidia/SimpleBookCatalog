using Mapster;
using Microsoft.AspNetCore.Components;
using SimpleBookCatalog.App.Models;
using SimpleBookCatalog.App.Interfaces;
using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.App.Pages;

public partial class Book
{
    [Inject]
    private IBookService _service { get; set; }
    
    [Inject]
    private NavigationManager _nvm { get; set; }
    
    [Parameter] public string? Id { get; set; }

    [Parameter] public string? Back { get; set; }

    public BookResponseDto BookModel { get; set; } = new();

    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            var bookId = new Guid(Id);
            var book = await _service.GetBookById(bookId);

            BookModel = book;
        }
        else
        {
            BookModel.PublicationDate = DateTime.Now.Date;
        }
    }
    
    private async Task HandleValidSubmit()
    {
        ServiceResult result;
        
        if (string.IsNullOrEmpty(Id)) //Add
        {
            var newBook = BookModel.Adapt<AddBookRequestDto>();
            result = await _service.AddBook(newBook);
        }
        else // Update
        {
            var updateBook = BookModel.Adapt<UpdateBookRequestDto>();
            result = await _service.UpdateBook(updateBook);
        }

        if (result.Check)
        {
            var backUrl = Back switch
            {
                "bks" => "books",
                "qgd" => "books-quick-grid",
                "rdz" => "books-radzen-grid",
                "sfg" => "books-syncfusion-grid",
                _ => "home"
            };

            _nvm.NavigateTo($"/{backUrl}");
        }

        Message = $"Coś poszło nie tak spróbuj ponownie!\n{result.Info}";
    }

    private void HandleInvalidSubmit()
    {
        Message = "Coś poszło nie tak :-(";
    }
}

