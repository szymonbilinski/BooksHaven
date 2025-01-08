using System.Collections.ObjectModel;
using System.Net.Http.Json;
using BooksHaven.Models;
using BooksHaven.Services;
using BooksHaven.Views;
using CommunityToolkit.Mvvm.Input;



namespace BooksHaven.ViewModels
{
    public partial class SearchBookPageViewModel :BaseViewModel
    {
        private GoogleBooksService _googleBooksSerice=new GoogleBooksService();
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
            
            SearchCommand = new Command(async () => await GetBooksFromAPI());
        }

        private async Task GetBooksFromAPI()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            Books.Clear();

            try
            {
                var response = await _googleBooksSerice.SearchBooksAsync(SearchQuery);
                if (response.Count() > 0)
                {
                    foreach (var book in response)
                    {
                        Books.Add(book);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("No results","No books found with this query", "OK");

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }
        [RelayCommand]
        async Task GoToDetailsAsync(BookModel book)
        {
            if (book is null)
            {
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Book",book }
                });
        }

    }
}
