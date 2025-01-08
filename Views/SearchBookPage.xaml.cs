using BooksHaven.ViewModels;

namespace BooksHaven.Views;

public partial class SearchBookPage : ContentPage
{
	public SearchBookPage()
	{
		InitializeComponent();
		BindingContext = new SearchBookPageViewModel();
	}
}