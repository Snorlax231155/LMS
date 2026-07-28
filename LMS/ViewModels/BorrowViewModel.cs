using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class BorrowViewModel
    {
        [Required]
        public int BookId { get; set; }

        [BindNever]
        public string? BookTitle { get; set; }

        [Required(ErrorMessage = "Your name is required.")]
        [StringLength(100)]
        public string? BorrowerName { get; set; }

        [Required(ErrorMessage = "Your email is required.")]
        [EmailAddress]
        public string? BorrowerEmail { get; set; }

        [Required(ErrorMessage = "Your Phone Number is required.")]
        [Phone]
        public string? Phone { get; set; }
    }
}
