using System;
using System.Collections.Generic;
using System.Linq;
using BillingApi.Data;

namespace BillingApi.Models
{
    public class CartService : ICartService
    {
        public Bill IssueBill(List<(Item dbItem, int qty)> items, string currency)
        {
            Bill bill = new Bill();
            foreach(var item in items)
            {
                bill.BillItems.Add(new BillItem()
                                {
                                    Name = item.dbItem.Name,
                                    Quantity = item.qty,
                                    Price = item.dbItem.Price,
                                    Discount = item.dbItem.Discount,
                                    AfterDiscountPrice = item.dbItem.Price - (item.dbItem.Price * item.dbItem.Discount / 100),
                                    TotalPrice = (item.qty * (item.dbItem.Price - (item.dbItem.Price * item.dbItem.Discount / 100)))
                });

                bill.SubTotal += bill.BillItems.Last().TotalPrice;
            }
            bill.Currency = currency;
            bill.Total = bill.SubTotal + (bill.SubTotal * bill.Vat / 100);
            return bill;
        }
    }
}