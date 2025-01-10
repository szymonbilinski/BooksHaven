using BooksHaven.Services;
using BooksHaven.ViewModels;
using BooksHaven.Views;
using Microsoft.Extensions.Logging;

namespace BooksHaven
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<GoogleBooksService>();

            builder.Services.AddTransient<BookDetailsPageViewModel>();
            builder.Services.AddTransient<BookDetailsPage>();

            builder.Services.AddTransient<LocalBooksDetailsPageViewModel>();
            builder.Services.AddTransient<LocalBookDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
