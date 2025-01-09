using BooksHaven.Models;
using System.Net.Http.Json;

namespace BooksHaven.Services
{
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        Constants constants = new Constants();

        public GoogleBooksService()
        {
            string apiKey = constants.APIKEY;
            _httpClient = new HttpClient();
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<List<BookModel>> SearchBooksByQueryAsync(string query,int startIndex)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be empty", nameof(query));
            }

            var url = $"https://www.googleapis.com/books/v1/volumes?q={query}&startIndex={startIndex}&maxResults=20&key={_apiKey}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<GoogleBooksApiResponse>(url);

                return response?.Items?.Select(item => new BookModel
                {
                    Title = item.VolumeInfo?.Title ?? "Title not available",
                    Authors = string.Join(", ", item.VolumeInfo?.Authors ?? new List<string>()) ?? "Author(s) not available",
                    Description = item.VolumeInfo?.Description ?? "Description not available",
                    PublishedDate = item.VolumeInfo?.PublishedDate ?? "Published Date not available",
                    Thumbnail = item.VolumeInfo?.ImageLinks?.Thumbnail != null
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
}
