using BooksHaven.ViewModels;
using System.ComponentModel;

namespace BooksHaven.Views;

public partial class BookDetailsPage : ContentPage , INotifyPropertyChanged
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