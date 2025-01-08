using BooksHaven.Models;
using BooksHaven.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
