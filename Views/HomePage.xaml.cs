using BooksHaven.ViewModels;

namespace BooksHaven.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
	}
}