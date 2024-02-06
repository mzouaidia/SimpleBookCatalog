using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using SimpleBookCatalog.App.Models;
using SimpleBookCatalog.App.Interfaces;
using SimpleBookCatalog.Application.Dtos;

namespace SimpleBookCatalog.App.Services;

public class BookService(HttpClient http, IConfiguration cfg) : IBookService
{
    private readonly string _basePath = cfg.GetSection("BasePath").Value ?? string.Empty;

    private readonly JsonSerializerOptions _jso = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<BookResponseDto>?> GetBooks()
    {
        try
        {
            var apiResp = await http.GetStreamAsync(_basePath);
            var books = await JsonSerializer.DeserializeAsync<List<BookResponseDto>>(apiResp, _jso);

            return books;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<BookResponseDto?> GetBookById(Guid id)
    {
        try
        {
            var book = await http.GetFromJsonAsync<BookResponseDto>(string.Concat(_basePath, id));

            return book;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ServiceResult> AddBook(AddBookRequestDto book)
    {
        try
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
            var response = await http.PostAsync(_basePath, bookJson);

            var result = new ServiceResult
            {
                Check = response.IsSuccessStatusCode,
                Info = response.StatusCode.ToString()
            };

            return result;

            //if (!response.IsSuccessStatusCode) return response.StatusCode.ToString();

            //return "SUCCESS";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ServiceResult> UpdateBook(UpdateBookRequestDto book)
    {
        try
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
            var response = await http.PutAsync(_basePath, bookJson);

            var result = new ServiceResult
            {
                Check = response.IsSuccessStatusCode,
                Info = response.StatusCode.ToString()
            };

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ServiceResult> DeleteBook(Guid id)
    {
        try
        {
            var response = await http.DeleteAsync(string.Concat(_basePath, id));

            var result = new ServiceResult
            {
                Check = response.IsSuccessStatusCode,
                Info = response.StatusCode.ToString()
            };

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}