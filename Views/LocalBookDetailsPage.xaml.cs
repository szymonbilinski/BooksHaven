using BooksHaven.ViewModels;
using System.ComponentModel;

namespace BooksHaven.Views;

public partial class LocalBookDetailsPage : ContentPage,INotifyPropertyChanged
{
	public LocalBookDetailsPage()
	{
		InitializeComponent();
		BindingContext = new LocalBooksDetailsPageViewModel();
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

}