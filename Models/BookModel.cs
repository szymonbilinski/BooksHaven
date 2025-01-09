

namespace BooksHaven.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public string? Authors { get; set; }
        public string? Description { get; set; }
        public string? PublishedDate { get; set; }
        public ImageSource? Thumbnail { get; set; }

        

    }
}
