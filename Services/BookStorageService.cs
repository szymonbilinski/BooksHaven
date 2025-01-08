using BooksHaven.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksHaven.Services
{
    public static class BookStorageService 
    {
        static SQLiteAsyncConnection db;
        public static async Task Init()
        {
            if (db != null)
            {
                return;
            }
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "BookStorage.db");
            db = new SQLiteAsyncConnection(databasePath);
            try
            {
                await db.CreateTableAsync<ReadBookModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }
        public static async Task AddBook(BookModel book)
        {
            await Init();
            var readbooklocal = new ReadBookModel
            {
                Title = book.Title,
                Authors = book.Authors,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Thumbnail = book.Thumbnail.ToString(),
                ReadDate = ""
            };
            var id = await db.InsertAsync(readbooklocal); 
        }
        public static async Task RemoveBook(ReadBookModel book)
        {
            await Init();

            await db.DeleteAsync<ReadBookModel>(book.Id);
        }
        public  static async Task<List<ReadBookModel>> GetBooks()
        {
            await Init();
            var LocalBooks = await db.Table<ReadBookModel>().ToListAsync();
            return LocalBooks;
        }
    }
}
