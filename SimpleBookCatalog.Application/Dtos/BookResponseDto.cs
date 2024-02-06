using SimpleBookCatalog.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using SimpleBookCatalog.Application.CustomValidators;

namespace SimpleBookCatalog.Application.Dtos;

public class BookResponseDto
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Author { get; set; }

    [Required]
    [CustomRange(ErrorMessage = "Value for {0} must be between {1:d} and {2:d}")]
    public DateTime PublicationDate { get; set; }
    
    public Category Category { get; set; }
}