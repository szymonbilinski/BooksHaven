using BooksHaven.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksHaven.ViewModels;

[QueryProperty("Book", "Book")]

public partial class BookDetailsPageViewModel : BaseViewModel
{
    public BookDetailsPageViewModel() 
    {
        
    }
    [ObservableProperty]
    BookModel book;
}
