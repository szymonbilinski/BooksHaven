using BooksHaven.Models;
using BooksHaven.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksHaven.ViewModels;

[QueryProperty(nameof(CurrentBook), "Book")]
public partial class BookDetailsPageViewModel : BaseViewModel
{
    public ICommand AddBookToLibraryCommand { get; }

    public BookDetailsPageViewModel()
    {
        AddBookToLibraryCommand = new AsyncCommand(AddBookToLibraryAsync);
      
    }


    [ObservableProperty]
    private BookModel currentBook;

    [ObservableProperty]
    private bool isBusy;


    partial void OnCurrentBookChanged(BookModel value)
    {
        // Optional: Log the book data when it changes
        Console.WriteLine($"Current Book Changed: {value?.Title}");
        OnPropertyChanged(nameof(CurrentBook));
    }


    private async Task AddBookToLibraryAsync()
    {
        if (currentBook == null)
        {
            await App.Current.MainPage.DisplayAlert("No books", "You didn't select Books", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var result = await BookStorageService.AddBookToStorageAsync(currentBook);
            if (result == true)
                await App.Current.MainPage.DisplayAlert("Added", "Book successfully added to your library.", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Wrong", "You already have this book in you library", "OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding book to library: {ex.Message}");
            await App.Current.MainPage.DisplayAlert("Error", "An error occurred while deleting the book. Please try again.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }


}
