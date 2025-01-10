using BooksHaven.ViewModels;
namespace BooksHaven.Views;

public partial class ReadBookPage : ContentPage
{
	public ReadBookPage()
	{
		InitializeComponent();
		BindingContext = new ReadBookPageViewModel();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as ReadBookPageViewModel).LoadBooksAsync();
    }
}