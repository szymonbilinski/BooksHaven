using System.Collections.ObjectModel;
using System.Net.Http.Json;
using BooksHaven.Models;

namespace BooksHaven.ViewModels
{
    public class SearchBookPageViewModel :BaseViewModel
    {
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        public ObservableCollection<BookModel> Books { get; set; } = new ObservableCollection<BookModel>();

        public Command SearchCommand { get; }

        public SearchBookPageViewModel()
        {
            SearchCommand = new Command(async () => await SearchBooksAsync());
        }

        private async Task SearchBooksAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            Books.Clear();

            try
            {
                using var client = new HttpClient();
                var response = await client.GetFromJsonAsync<BookFromApiModel>(
                    $"https://www.googleapis.com/books/v1/volumes?q={SearchQuery}");

                if (response?.Items != null)
                {
                    foreach (var item in response.Items)
                    {
                        Books.Add(new BookModel
                        {
                            Title = item.VolumeInfo.Title ?? "No Title Available",
                            Authors = item.VolumeInfo.Authors != null
                                ? string.Join(", ", item.VolumeInfo.Authors)
                                : "Unknown Author",
                            Thumbnail = item.VolumeInfo.ImageLinks?.Thumbnail ?? "placeholder_image.png"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
