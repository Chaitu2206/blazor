using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable

namespace Zindagi.Domain.UserAggregate.ViewModels
{
    [DebuggerDisplay("{Email}:{Id}", Name = "User DTO")]
    public class UserDto
    {
        [Editable(false)]
        [Display(Name = "User ID")]
        public long Id { get; set; }

        [Editable(false)]
        [Display(Name = "Alternate ID")]
        public string AlternateId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The first name should be maximum 20 characters long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(25, ErrorMessage = "The middle name should be maximum 25 characters long")]
        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "The last name should be maximum 25 characters long")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public int BloodGroup { get; set; }

        [Editable(false)]
        [Display(Name = "Email Verified")]
        public bool IsEmailVerified { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
