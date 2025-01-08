using BooksHaven.Models;
using BooksHaven.Services;
using BooksHaven.Views;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;


namespace BooksHaven.ViewModels;

public partial class ReadBookPageViewModel : BaseViewModel
{
    public ObservableRangeCollection<ReadBookModel> LocalBooks { get; set; }
    public AsyncRelayCommand GetLocalBooksCommand { get; }


    public ReadBookPageViewModel()
    {
        GetLocalBooksCommand = new AsyncRelayCommand(GetLocalBooks);
        LocalBooks = new ObservableRangeCollection<ReadBookModel>();
    }


    async Task GetLocalBooks()
    {
        LocalBooks.Clear();
        var localbookstemp = await BookStorageService.GetBooks();
        LocalBooks.AddRange(localbookstemp);

    }
    [RelayCommand]
    async Task GoToDetailsAsync(ReadBookModel localbook)
    {
        if (localbook is null)
        {
            return;
        }
        await Shell.Current.GoToAsync($"{nameof(LocalBookDetailsPage)}", true,
            new Dictionary<string, object>
            {
                    {"Book",localbook }
            });
    }
}
