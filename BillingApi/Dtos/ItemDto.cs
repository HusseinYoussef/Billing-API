using System;
using System.ComponentModel.DataAnnotations;

namespace BillingApi.Dtos
{
    public class ItemDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        [Required]
        public double? Price { get; set; }

        [Range(0, 100, ErrorMessage="Discount value should be between 0% and 100%")]
        public int Discount {get; set;}
    }
}