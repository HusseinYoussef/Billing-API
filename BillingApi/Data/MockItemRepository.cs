using System;
using System.Collections.Generic;
using System.Linq;
using BillingApi.Models;

namespace BillingApi.Data
{
    public class MockItemRepository : IItemRepository
    {
        private List<Item> _items;

        public MockItemRepository()
        {
            _items = new List<Item>()
            {
                new Item(){Id=1, Name="Shoes", Price=100, Discount=10},
                new Item(){Id=2, Name="T-Shirt", Price=240.5, Discount=0},
                new Item(){Id=3, Name="Pants", Price=200, Discount=10},
                new Item(){Id=4, Name="Hat", Price=70, Discount=0}
            };
        }

        public List<Item> GetAllItems()
        {
            return _items;
        }
        public Item GetItemById(int id)
        {
            return _items.FirstOrDefault(item => item.Id==id);
        }

        public int AddItem(Item newItem)
        {
            Item reItem = _items.FirstOrDefault(item => item.Name==newItem.Name);
            if(reItem != null)
            {
                throw new Exception("Item Name exists");
            }
            newItem.Id = _items.Max(Item => Item.Id) + 1;
            _items.Add(newItem);
            return newItem.Id;
        }

        public Item UpdateItem(Item updatedItem)
        {
            Item item = _items.FirstOrDefault(item => item.Id==updatedItem.Id);
            if(item != null)
            { 
                item = updatedItem;
            }
            return updatedItem;
        }

        public void DeleteItem(Item item)
        {
            _items.Remove(item);
        }

        public List<Item> GetCartItemsByNames(List<CartItem> cartItems)
        {
            List<string> names = cartItems.Select(cartItem => cartItem.Name).ToList();

            var result = _items.Where(Item => names.Contains(Item.Name)).ToList();
            
            return result;
        }

        public Item GetItemByName(string name)
        {
            return _items.FirstOrDefault(i => i.Name == name);
        }
    }
}