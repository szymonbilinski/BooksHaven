using BooksHaven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BooksHaven.Services;

public class GoogleBooksService
{
    private readonly HttpClient _httpClient;

    public GoogleBooksService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<BookModel>> SearchBooksAsync(string query)
    {
        string apiKey = "APIKEY"; // Replace with your actual API key
        string url = $"https://www.googleapis.com/books/v1/volumes?q={query}&key={apiKey}";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<BookFromApiModel>(url);
            return response?.Items?.Select(item => new BookModel
            {
                Title = item.VolumeInfo.Title ?? "Title not available",
                Authors = string.Join(", ", item.VolumeInfo.Authors ?? new List<string>()) ?? "Author(s) not available",
                Description = item.VolumeInfo.Description ?? "Description not available",
                PublishedDate = item.VolumeInfo.PublishedDate ?? "Published Date not available",
                Thumbnail = item.VolumeInfo.ImageLinks?.Thumbnail != null
                        ? ImageSource.FromUri(new Uri(item.VolumeInfo.ImageLinks.Thumbnail))
                        : ImageSource.FromUri(new Uri("https://dummyimage.com/300x200/ff5733/ffffff&text=No+Image"))
            }).ToList() ?? new List<BookModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching books: {ex.Message}");
            return new List<BookModel>(); 
        }
    }
}

