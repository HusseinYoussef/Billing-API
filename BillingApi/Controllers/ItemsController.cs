using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillingApi.Models;
using BillingApi.Data;
using AutoMapper;
using BillingApi.Dtos;
using Microsoft.AspNetCore.Http;

namespace BillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, ICartService cartService, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ItemDto>> GetItems()
        {
            IEnumerable<Item> items = _itemRepository.GetAllItems();
            if(items.Count() == 0)
            {
                return NotFound("No Items Found");
            }
            return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ItemDto> GetItem(int id)
        {
            Item item = _itemRepository.GetItemById(id);
            if(item == null)
            {
                return NotFound("Item Not Found");
            }
            return Ok(_mapper.Map<ItemDto>(item));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Item> CreateItem(ItemDto item)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("One or more parameters are invalid");
            }

            var reItem = _itemRepository.GetItemByName(item.Name);
            if(reItem != null)
            {
                return Conflict("Error Occured");
            }
            Item dbItem = _mapper.Map<Item>(item);
            int createdId = _itemRepository.AddItem(dbItem);
            return CreatedAtAction(nameof(GetItem), new {id=createdId}, _mapper.Map<ItemDto>(dbItem));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult UpdateItem(int id, ItemDto itemDto)
        {
            Item dbItem = _itemRepository.GetItemById(id);
            if(dbItem == null)
            {
                return NotFound("Item Not Found");
            }

            _mapper.Map(itemDto, dbItem);
            try
            {
                _itemRepository.UpdateItem(dbItem);
            }
            catch
            {
                return Conflict();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeleteItem(int id)
        {
            Item item = _itemRepository.GetItemById(id);
            if(item == null)
            {
                return NotFound("Item Not Found");
            }
            try
            {
                _itemRepository.DeleteItem(item);
            }
            catch
            {
                return Conflict("Error Occured");
            }
            return NoContent();
        }
    }
}