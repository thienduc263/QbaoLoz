using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Member
    {
        [Key]
        [Display(Name = "Member ID")]
        public int MemberId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
