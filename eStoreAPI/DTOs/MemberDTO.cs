using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.DTOs
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
