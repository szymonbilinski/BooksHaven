using BooksHaven.Models;
using BooksHaven.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers.Commands;


namespace BooksHaven.ViewModels;

[QueryProperty("Book", "Book")]
public partial class LocalBooksDetailsPageViewModel : BaseViewModel
{
    public AsyncCommand DeleteLocalBookCommand { get; }
    public LocalBooksDetailsPageViewModel() 
    {
        DeleteLocalBookCommand = new AsyncCommand(DeleteLocalBook);
    }
    [ObservableProperty]
    ReadBookModel book;

    async Task DeleteLocalBook()
    {
        await BookStorageService.RemoveBook(book);
    }

    
}
