using BooksHaven.ViewModels;

namespace BooksHaven.Views;

public partial class BookDetailsPage : ContentPage
{
	public BookDetailsPage(BookDetailsPageViewModel viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}