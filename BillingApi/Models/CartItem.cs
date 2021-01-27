using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillingApi.Models
{
    public class CartItem
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0, Int32.MaxValue)]
        public int? Quantity { get; set; }
    }
}