using System.Collections.ObjectModel;
using System.Net.Http.Json;
using BooksHaven.Models;
using BooksHaven.Services;
using BooksHaven.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BooksHaven.ViewModels;

public partial class SearchBookPageViewModel : BaseViewModel
{
    private readonly GoogleBooksService _googleBooksService;

    public ObservableCollection<BookModel> Books { get; }

    public AsyncRelayCommand SearchBooksCommand { get; }
    public AsyncRelayCommand<BookModel> NavigateToDetailsCommand { get; }

    public SearchBookPageViewModel()
    {
        _googleBooksService = new GoogleBooksService();
        Books = new ObservableCollection<BookModel>();

        SearchBooksCommand = new AsyncRelayCommand(SearchBooksAsync);
        NavigateToDetailsCommand = new AsyncRelayCommand<BookModel>(NavigateToDetailsAsync);
    }

    [ObservableProperty]
    private string searchQuery;

    [ObservableProperty]
    private bool isBusy;


    private async Task SearchBooksAsync()
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return;
        }

        if (isBusy) return;

        isBusy = true;
        Books.Clear();

        try
        {
            var results = await _googleBooksService.SearchBooksByQueryAsync(SearchQuery);
            if (results.Any())
            {
                foreach (var book in results)
                {
                    Books.Add(book);
                }            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching books: {ex.Message}");
        }
        finally
        {
            isBusy = false;
        }
    }
    private async Task NavigateToDetailsAsync(BookModel book)
    {
        if (book is null)
        {
            return;
        }

        try
        {
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}", true, new Dictionary<string, object>
            {
                { "Book", book }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating to details page: {ex.Message}");
        }
    }


}
