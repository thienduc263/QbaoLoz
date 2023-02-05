using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Product
    {   
        [Key]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Weight (gram)")]
        public double Weight { get; set; }
        [Required]
        [Display(Name = "Unit Price ($)")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Display(Name = "Unit In Stock")]
        public int UnitInStock { get; set; }
        public virtual Category Category { get; set; }
    }
}
