using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BillingApi.Validations;

namespace BillingApi.Models
{
    public class Bill
    {
        public List<BillItem> BillItems {get; set;}

        public double SubTotal { get; set; }

        [JsonPropertyName("VAT")]
        public int Vat {get; set;}

        public string Currency {get; set;}

        public double Total {get; set;}

        public Bill()
        {
            BillItems = new List<BillItem>();
            Vat = 14;
            SubTotal = Total = 0;
        }
    }
}