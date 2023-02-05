using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderDetail
    {   
        [Key, Column(Order = 0)]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Discount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
