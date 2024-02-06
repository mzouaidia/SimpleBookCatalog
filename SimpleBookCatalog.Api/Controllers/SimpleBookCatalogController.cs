using Microsoft.AspNetCore.Mvc;
using SimpleBookCatalog.Application.Dtos;
using SimpleBookCatalog.Application.Services;

namespace SimpleBookCatalog.Api.Controllers;

[ApiController]
[Route("api/books")]
public class SimpleBookCatalogController(IServiceManager service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAsync(AddBookRequestDto dto)
    {
       await service.BookService.AddAsync(dto);

        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetByIdAsync(Guid id)
    {
        var result = await service.BookService.GetByIdAsync(id);

        return result is null ? NotFound() : result;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<BookResponseDto>>> GetAllAsync()
    {
        var result = await service.BookService.GetAllAsync();

        return result is null ? NotFound() : result;
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateBookRequestDto book)
    {
        var result = await service.BookService.UpdateAsync(book);

        return result ? Ok() : NotFound();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await service.BookService.DeleteByIdAsync(id);
        
        return result ? Ok() : NotFound();
    }
    
    [HttpGet("GetTest")]
    public Task<JsonResult> GetTest()
    {
        return Task.FromResult(new JsonResult("Testowo kolorowo :-)"));
    }
}
