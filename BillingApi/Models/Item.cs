using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BillingApi.Validations;

namespace BillingApi.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        public string Manufacturer { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage="Discount value should be between 0% and 100%")]
        public int Discount {get; set;}
    }
}