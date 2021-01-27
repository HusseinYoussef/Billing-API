using System;
using System.Collections.Generic;
using BillingApi.Models;

namespace BillingApi.Data
{
    public interface IItemRepository
    {
        List<Item> GetAllItems();
        List<Item> GetCartItemsByNames(List<CartItem> cartItems);
        Item GetItemById(int id);
        Item GetItemByName(string name);
        int AddItem(Item newItem);
        Item UpdateItem(Item updatedItem);
        void DeleteItem(Item item);
    }
}