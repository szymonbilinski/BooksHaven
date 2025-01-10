using BooksHaven.Views;

namespace BooksHaven
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BookDetailsPage),typeof(BookDetailsPage));
            Routing.RegisterRoute(nameof(LocalBookDetailsPage), typeof(LocalBookDetailsPage));
        }
    }
}
