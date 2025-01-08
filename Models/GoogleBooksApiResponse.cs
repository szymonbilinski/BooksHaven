namespace BooksHaven.Models
{
    public class GoogleBooksApiResponse
    {
        public List<BookItem> Items { get; set; } = new List<BookItem>(); 
    }

    public class BookItem
    {
        public VolumeInfo VolumeInfo { get; set; } 
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string>? Authors { get; set; } 
        public ImageLinks? ImageLinks { get; set; } 
        public string? PublishedDate { get; set; } 
        public string? Description { get; set; } 
    }

    public class ImageLinks
    {
        public string? Thumbnail { get; set; } 
    }
}
