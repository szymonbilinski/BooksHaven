using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksHaven.ViewModels;

public partial class HomePageViewModel :BindableObject
{
    [RelayCommand]
    private async Task NavigateToSearchPage()
    {
        await Shell.Current.GoToAsync("//SearchBookPage");
    }
}
