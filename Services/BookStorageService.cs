using BooksHaven.Models;
using SQLite;
using System.Collections.Generic;

namespace BooksHaven.Services
{
    public static class BookStorageService
    {
        private static SQLiteAsyncConnection? _db;

        public static async Task InitializeAsync()
        {
            if (_db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "BookStorage.db");
            _db = new SQLiteAsyncConnection(databasePath);

            try
            {
                await _db.CreateTableAsync<ReadBookModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
            }
        }

        public static async Task<bool> AddBookToStorageAsync(BookModel book)
        {
            await InitializeAsync();

            var existingBooks = await _db.Table<ReadBookModel>().Where(x => x.Title == book.Title && x.Authors == book.Authors && x.PublishedDate == book.PublishedDate).ToListAsync();

            if(existingBooks.Count > 0) { return false; }

            var readBook = new ReadBookModel
            {
                Title = book.Title,
                Authors = book.Authors ?? string.Empty,
                Description = book.Description ?? string.Empty,
                PublishedDate = book.PublishedDate ?? string.Empty,
                Thumbnail = book.Thumbnail?.ToString().Remove(0,5) ?? string.Empty,
                ReadDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            try
            {
                await _db.InsertAsync(readBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
            }
            return true;
        }

        public static async Task RemoveBookFromStorageAsync(ReadBookModel book)
        {
            await InitializeAsync();



            try
            {
                await _db.DeleteAsync<ReadBookModel>(book.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing book: {ex.Message}");
            }
        }

        public static async Task<List<ReadBookModel>> GetAllBooksAsync()
        {
            await InitializeAsync();

            try
            {
                var localBooks = await _db.Table<ReadBookModel>().ToListAsync();
                return localBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving books: {ex.Message}");
                return new List<ReadBookModel>();
            }
        }
    }
}
