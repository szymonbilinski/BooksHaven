using BooksHaven.ViewModels;
namespace BooksHaven.Views;

public partial class ReadBookPage : ContentPage
{
	public ReadBookPage()
	{
		InitializeComponent();
		BindingContext = new ReadBookPageViewModel();
	}
}