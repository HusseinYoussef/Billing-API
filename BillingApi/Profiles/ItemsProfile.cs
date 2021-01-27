using System;
using System.Collections.Generic;
using AutoMapper;
using BillingApi.Dtos;
using BillingApi.Models;

namespace EmploymentWebApp.Profiles
{
    public class ItemsProfile: Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
        }
    }
}