using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public AsyncRelayCommand GoToPreviousPageCommand { get; }
    public AsyncRelayCommand GoToNextPageCommand { get; }



    public SearchBookPageViewModel()
    {
        _googleBooksService = new GoogleBooksService();
        Books = new ObservableCollection<BookModel>();

        SearchBooksCommand = new AsyncRelayCommand(SearchBooksAsync);
        NavigateToDetailsCommand = new AsyncRelayCommand<BookModel>(NavigateToDetailsAsync);

        GoToPreviousPageCommand = new AsyncRelayCommand(GoToPreviousPage);
        GoToNextPageCommand = new AsyncRelayCommand(GoToNextPage);
    }

    [ObservableProperty]
    private string searchQuery;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private int currentPage = 1;




    private int startSearchIndex = 0;

   

    private async Task GoToPreviousPage()
    {
        if (currentPage > 1)
        {
            
            CurrentPage--;
            await Task.Delay(1);
            await SearchBooksAsync();
            OnPropertyChanged(nameof(CurrentPage));

        }
        return;
    }

    private async Task GoToNextPage()
    {
        CurrentPage++;
        await Task.Delay(1);
        await SearchBooksAsync();
        OnPropertyChanged(nameof(CurrentPage));
        return;
    }

    private bool CheckForInternetConnection()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if(accessType == NetworkAccess.Internet)
        {
            return true;
        }
        return false;
    }

    private async Task SearchBooksAsync()
    {
        if (CheckForInternetConnection() is false)
        {
            await App.Current.MainPage.DisplayAlert("No Internet Connection", "Please Check your internet connection", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            await App.Current.MainPage.DisplayAlert("Wrong", "Plase provide book title or authors", "OK");
            return;
        }

        if (isBusy) return;

        isBusy = true;
        Books.Clear();

        try
        {
            var startSearchIndexHelper = (currentPage - 1) * 20;
            var results = await _googleBooksService.SearchBooksByQueryAsync(SearchQuery,startSearchIndex+startSearchIndexHelper);
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
            await App.Current.MainPage.DisplayAlert("Error", "An error occurred while searching for books.", "OK");
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
            await App.Current.MainPage.DisplayAlert("Error", "An error occurred while navigating to the details page.", "OK");
        }
    }


}
