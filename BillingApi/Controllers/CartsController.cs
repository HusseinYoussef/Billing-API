using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillingApi.Data;
using BillingApi.Models;

namespace BillingApi.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICartService _cartService;

        public CartsController(IItemRepository itemRepository, ICartService cartSevice)
        {
            _itemRepository = itemRepository;
            _cartService = cartSevice;
        }

        [HttpPost]
        public ActionResult<Bill> Checkout(Cart cart)
        {
            List<(Item, int)> items = new List<(Item, int)>();
            foreach(var cartItem in cart.CartItems)
            {
                Item item = _itemRepository.GetItemByName(cartItem.Name);
                if(item == null)
                {
                    return NotFound("Not Found: Invalid Item Provided");
                }
                items.Add((item, (int)cartItem.Quantity));
            }

            try
            {
                Bill bill = _cartService.IssueBill(items, cart.Currency);
                return Ok(bill);
            }
            catch
            {
                return Conflict("Error Occured");
            }
        }
    }
}