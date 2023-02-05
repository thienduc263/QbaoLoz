using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        [Required]
        public DateTime ShippedDate { get; set; }
        [Required]
        public decimal Freight { get; set; }
    }
}
