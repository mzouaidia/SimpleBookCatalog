using SimpleBookCatalog.Domain.Enums;

namespace SimpleBookCatalog.Application.Dtos;

public class UpdateBookRequestDto
{
    public Guid Id { get; set; }
 
    public string? Title { get; set; }

    public string? Author { get; set; }
    
    public DateTime? PublicationDate { get; set; }
    
    public Category Category { get; set; }
}