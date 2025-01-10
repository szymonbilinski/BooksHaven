using SQLite;

namespace BooksHaven.Models
{
    public class ReadBookModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ReadDate { get; set; }
        public string Title { get; set; }
        public string? Authors { get; set; }
        public string? Description { get; set; }
        public string? PublishedDate { get; set; }
        public string? Thumbnail { get; set; }
    }
}
