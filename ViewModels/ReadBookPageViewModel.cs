using BooksHaven.Models;
using BooksHaven.Services;
using BooksHaven.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksHaven.ViewModels;

public partial class ReadBookPageViewModel : BaseViewModel
{
    // Collection for displaying local books
    public ObservableRangeCollection<ReadBookModel> LocalBooks { get; set; }

    // Commands
    public AsyncRelayCommand GetLocalBooksCommand { get; }
    public ICommand NavigateToDetailsCommand { get; }

    public ReadBookPageViewModel()
    {
        LocalBooks = new ObservableRangeCollection<ReadBookModel>();
        GetLocalBooksCommand = new AsyncRelayCommand(LoadBooksAsync);
        NavigateToDetailsCommand = new AsyncRelayCommand<ReadBookModel>(NavigateToDetailsAsync);
    }

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string statusMessage;

    /// <summary>
    /// Loads books from local storage into the LocalBooks collection.
    /// </summary>
    private async Task LoadBooksAsync()
    {
        if (isBusy) return;

        isBusy = true;
        try
        {
            LocalBooks.Clear();
            var booksFromStorage = await BookStorageService.GetAllBooksAsync();
            if (booksFromStorage != null && booksFromStorage.Count > 0)
            {
                LocalBooks.AddRange(booksFromStorage);
                ShowMessage($"{booksFromStorage.Count} book(s) loaded.");
            }
            else
            {
                ShowMessage("No books found in your library.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books: {ex.Message}");
            ShowMessage("An error occurred while loading books. Please try again.");
        }
        finally
        {
            isBusy = false;
        }
    }


    private async Task NavigateToDetailsAsync(ReadBookModel selectedBook)
    {
        if (selectedBook == null)
        {
            ShowMessage("No book selected.");
            return;
        }

        try
        {
            await Shell.Current.GoToAsync($"{nameof(LocalBookDetailsPage)}", true, new Dictionary<string, object>
            {
                { "Book", selectedBook }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating to details page: {ex.Message}");
            ShowMessage("An error occurred while navigating to the details page.");
        }
    }

    /// <summary>
    /// Updates the status message for the user interface.
    /// </summary>
    /// <param name="message">Message to display.</param>
    private void ShowMessage(string message)
    {
        statusMessage = message;
    }
}
