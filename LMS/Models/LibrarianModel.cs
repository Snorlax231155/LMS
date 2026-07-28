using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class LibrarianModel
    {
        [Key]
        public int LibrarianId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
    }
}
