namespace LMS.Models
{
    public class DashboardModel
    {
        public int TotalStudents { get; set; }
        public int TotalBooks { get; set; }
        public int TotalLibrarians { get; set; }
        public int TotalBorrowings { get; set; }
        // Number of borrow records where ReturnDate is null (currently borrowed)
        public int CurrentBorrowed { get; set; }
    }
}
