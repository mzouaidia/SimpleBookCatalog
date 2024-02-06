using SimpleBookCatalog.Domain.Enums;

namespace SimpleBookCatalog.Application.Dtos;

public class AddBookRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;
    
    public DateTime PublicationDate { get; set; }
    
    public Category Category { get; set; }
}