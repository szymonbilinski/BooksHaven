using BooksHaven.ViewModels;

namespace BooksHaven.Views;

public partial class LocalBookDetailsPage : ContentPage
{
	public LocalBookDetailsPage()
	{
		InitializeComponent();
		BindingContext = new LocalBooksDetailsPageViewModel();
	}
    
}