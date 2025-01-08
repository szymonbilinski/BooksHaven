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

    [ObservableProperty]
    private string statusMessage;

    private async Task DeleteBookFromLibraryAsync()
    {
        if (selectedBook == null)
        {
            ShowMessage("No book selected to delete.");
            return;
        }

        IsBusy = true;
        try
        {
            await BookStorageService.RemoveBookFromStorageAsync(SelectedBook);
            ShowMessage("Book successfully removed from your library.");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            ShowMessage("An error occurred while deleting the book. Please try again.");
        }
        finally
        {
            IsBusy = false;
        }
        IsBusy = false;
    }

    private void ShowMessage(string message)
    {
        StatusMessage = message;
    }
}
