﻿using System.Collections.ObjectModel;
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

    public ObservableCollection<BookModel> Books { get; set; }

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

    [ObservableProperty]
    private int currentPage=1;
    [ObservableProperty]
    private int pickerSelectedIndex = -1;

    private int startIndex = 0;

    partial void OnPickerSelectedIndexChanged(int value)
    {
        var picker = value;

        switch (picker)
        {
            case 0:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderBy(i=>i.Title));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            case 1:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderByDescending(i => i.Title));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            case 2:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderBy(i => i.Authors));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            case 3:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderByDescending(i => i.Authors));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            case 4:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderBy(i => i.PublishedDate));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            case 5:
                {
                    var BooksSorted = new ObservableCollection<BookModel>(Books.OrderByDescending(i => i.PublishedDate));
                    SortBooksAsync(BooksSorted);
                    OnPropertyChanged(nameof(Books));
                    break;
                }
            default: break;


        }
    }

    private async Task SortBooksAsync(ObservableCollection<BookModel> books)
    {
        Books.Clear();
        Books = books;

    }

    [RelayCommand]
    private async Task GoToNextPage()
    {
        currentPage++;
        await SearchBooksAsync();
        pickerSelectedIndex = -1;
        OnPropertyChanged(nameof(CurrentPage));
        OnPropertyChanged(nameof(PickerSelectedIndex));
    }
    [RelayCommand]
    private async Task GoToPreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            await SearchBooksAsync();
            pickerSelectedIndex = -1;
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(PickerSelectedIndex));
        }
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
            var startIndexHelper = startIndex + ((currentPage - 1)*20);
            var results = await _googleBooksService.SearchBooksByQueryAsync(SearchQuery,startIndexHelper);
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
