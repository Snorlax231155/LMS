namespace LMS.Models
{
    public class Magazine
    {
        public int MagazineId { get; set; }
        public string? Title { get; set; }
        public string? Publisher { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
