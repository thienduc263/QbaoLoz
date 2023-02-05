using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Order
    {   
        [Key]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Member ID")]
        public int MemberId { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }
        [Required]
        [Display(Name = "Shipped Date")]
        public DateTime ShippedDate { get; set; }
        [Required]
        public decimal Freight { get; set; }
        public virtual Member Member { get; set; }
    }
}
