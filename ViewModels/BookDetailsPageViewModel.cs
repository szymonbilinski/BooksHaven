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

    [ObservableProperty]
    private string statusMessage;

    private async Task AddBookToLibraryAsync()
    {
        if (currentBook == null)
        {
            ShowMessage("No book selected to add.");
            return;
        }

        IsBusy = true;
        try
        {
            await BookStorageService.AddBookToStorageAsync(currentBook);
            ShowMessage("Book successfully added to your library.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding book to library: {ex.Message}");
            ShowMessage("An error occurred while adding the book. Please try again.");
        }
        finally
        {
            IsBusy = false;
        }
    }


    private void ShowMessage(string message)
    {
        StatusMessage = message;
    }
}
