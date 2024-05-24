namespace elm_test.Models
{
    public class Book
    {
        public string BookTitle { get; set; }

        public string BookDescription { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }

        public string CoverBase64 { get; set; }

        public DateTime LastModified { get; set; }
    }
}