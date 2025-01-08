using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksHaven.Models
{
    public class BookFromApiModel
    {
        public List<Item> Items { get; set; }
    }
    public class Item
    {
        public VolumeInfo VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
    }

    public class ImageLinks
    {
        public string Thumbnail { get; set; }
    }
}
