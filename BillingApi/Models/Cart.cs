using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BillingApi.Data;
using BillingApi.Validations;

namespace BillingApi.Models
{
    public class Cart
    {
        [Required]
        public List<CartItem> CartItems {get; set;}

        [Required]
        [CurrencyName(ErrorMessage="Invalid Currency, Available (USD, EGP, EUR, JPY, CAD)")]
        public string Currency { get; set; }
    }
}