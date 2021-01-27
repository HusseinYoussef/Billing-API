using System;

namespace BillingApi.Models
{
    public class BillItem
    {
        public string Name {get; set;}

        public double Price {get; set;}
        
        public int Discount {get; set;}

        public double AfterDiscountPrice {get; set;}

        public int Quantity { get; set; }

        public double TotalPrice {get; set;}
    }
}