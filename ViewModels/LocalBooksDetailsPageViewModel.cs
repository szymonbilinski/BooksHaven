using BooksHaven.Models;
using BooksHaven.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksHaven.ViewModels;

[QueryProperty("SelectedBook", "Book")]
public partial class LocalBooksDetailsPageViewModel : BaseViewModel
{
    public ICommand DeleteBookFromLibraryCommand { get; }

    public LocalBooksDetailsPageViewModel()
    {
        DeleteBookFromLibraryCommand = new AsyncCommand(DeleteBookFromLibraryAsync);
    }

    [ObservableProperty]
    private ReadBookModel selectedBook;

    [ObservableProperty]
    private bool isBusy=true;

    partial void OnSelectedBookChanged(ReadBookModel value)
    {
        // Optional: Log the book data when it changes
        Console.WriteLine($"Current Book Changed: {value?.Title}");
        OnPropertyChanged(nameof(SelectedBook));
    }
    private async Task DeleteBookFromLibraryAsync()
    {
        if (selectedBook == null)
        {
            await App.Current.MainPage.DisplayAlert("No books", "You didn't select Books", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            await BookStorageService.RemoveBookFromStorageAsync(SelectedBook);
            await App.Current.MainPage.DisplayAlert("Deleted", "Book successfully removed from your library.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            await App.Current.MainPage.DisplayAlert("Error", "An error occurred while deleting the book. Please try again.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

}
