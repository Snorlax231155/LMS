namespace LMS.Models
{
    public class Newspaper
    {
        public int NewspaperId { get; set; }
        public string? Title { get; set; }
        public string? Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
