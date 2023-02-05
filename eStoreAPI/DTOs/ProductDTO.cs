using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int UnitInStock { get; set; }
    }
}
