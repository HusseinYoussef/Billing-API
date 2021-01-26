using System;
using System.Collections.Generic;
using BillingApi.Data;

namespace BillingApi.Models
{
    public interface ICartService
    {
        Bill IssueBill(IItemRepository itemRepository, Cart cart);
    }
}