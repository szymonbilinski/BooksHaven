using BooksHaven.Models;
using BooksHaven.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers.Commands;

namespace BooksHaven.ViewModels;

[QueryProperty("Book", "Book")]

public partial class BookDetailsPageViewModel : BaseViewModel
{
    public AsyncCommand AddLocalBookCommand { get; }
    public BookDetailsPageViewModel() 
    {
        AddLocalBookCommand = new AsyncCommand(AddLocalBook);
    }
    [ObservableProperty]
    BookModel book;

    async Task AddLocalBook()
    {
        await BookStorageService.AddBook(book);
    }
}
