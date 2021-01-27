using System;
using System.Linq;
using System.Collections.Generic;
using BillingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Data
{
    public class SqlItemRepository : IItemRepository
    {
        private readonly BillAppDbContext _context;

        public SqlItemRepository(BillAppDbContext context)
        {
            _context = context;
        }

        public int AddItem(Item newItem)
        {
            _context.Items.Add(newItem);
            _context.SaveChanges();
            return newItem.Id;
        }

        public void DeleteItem(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public List<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public List<Item> GetCartItemsByNames(List<CartItem> cartItems)
        {
            List<string> names = cartItems.Select(cItem => cItem.Name).ToList();
            List<Item> items = _context.Items.Where(item => names.Contains(item.Name)).ToList();
            return items;
        }

        public Item GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(item => item.Id==id);
        }

        public Item GetItemByName(string name)
        {
            return _context.Items.FirstOrDefault(item => item.Name==name);
        }

        public Item UpdateItem(Item updatedItem)
        {
            _context.SaveChanges();
            return updatedItem;
        }
    }
}