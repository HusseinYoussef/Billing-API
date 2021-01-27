using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BillingApi.Controllers;
using BillingApi.Data;
using BillingApi.Dtos;
using BillingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BillingApi.Tests
{
    public class ApiItemsControllerTests
    {
        [Fact]
        public void GetItem_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns((Item)null);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);

            // Act
            var result = controller.GetItem(testId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetItem_ReturnsOk_ForFoundItem()
        {
            // Arrange
            int testId = 5;
            string testName = "Shoes";
            Item testItem = new Item(){Id=testId, Name=testName, Price=100, Discount=10};
            var mock = new Mock<IItemRepository>();
            var mapper_mock = new Mock<IMapper>();
            mock.Setup(repo => repo.GetItemById(testId)).Returns(testItem);
            mapper_mock.Setup(mapper => mapper.Map<ItemDto>(testItem)).Returns(new ItemDto(){Name=testName});
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper_mock.Object);

            // Act
            var result = controller.GetItem(testId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var item = Assert.IsType<ItemDto>(okResult.Value);
            Assert.Equal(testName, item.Name);
        }

        [Fact]
        public void GetItems_ReturnsOk_ForFoundItems()
        {
            // Arrange
            List<Item> items = new List<Item>() 
                                {
                                    new Item(){Id=1, Name="Shoes", Price=100, Discount=10},
                                    new Item(){Id=2, Name="T-Shirt", Price=240.5, Discount=0},
                                    new Item(){Id=3, Name="Pants", Price=200, Discount=10},
                                };
            var mock = new Mock<IItemRepository>();
            var mock_mapper = new Mock<IMapper>();
            mock.Setup(repo => repo.GetAllItems()).Returns(items);
            mock_mapper.Setup(mapper => mapper.Map<IEnumerable<ItemDto>>(items))
                        .Returns(new List<ItemDto>()
                                    {
                                        new ItemDto(),
                                        new ItemDto(),
                                        new ItemDto()
                                    });
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mock_mapper.Object);

            // Act
            var result = controller.GetItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resItems = Assert.IsType<List<ItemDto>>(okResult.Value);
            Assert.Equal(items.Count(), resItems.Count());
        }

        [Fact]
        public void GetItems_ReturnsNotFound_ForNoItems()
        {
            // Arrange
            List<Item> items = new List<Item>();
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetAllItems()).Returns(items);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);

            // Act
            var result = controller.GetItems();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void CreateItem_ReturnsCreate_ForValidItem()
        {
            // Arrange
            ItemDto testItem = new ItemDto() {Name="NewItem", Price=50, Discount=0};
            var mock = new Mock<IItemRepository>();
            var mock_mapper = new Mock<IMapper>();
            mock.Setup(repo => repo.AddItem(new Item())).Returns(It.IsAny<int>());
            mock_mapper.Setup(mapper => mapper.Map<Item>(testItem)).Returns(new Item());
            mock_mapper.Setup(mapper => mapper.Map<ItemDto>(It.IsAny<Item>())).Returns(new ItemDto());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mock_mapper.Object);
            
            // Act
            var result = controller.CreateItem(testItem);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.IsType<ItemDto>(createdResult.Value);
        }

        [Fact]
        public void CreateItem_ReturnsBadRequest_ForInvalidItem()
        {
            // Arrange
            var mock = new Mock<IItemRepository>();
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.CreateItem(item: null);

            // Assign
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateItem_ReturnsUnprocessable_ForRepeatedItem()
        {
            // Assign
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemByName(It.IsAny<string>())).Returns(new Item());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);

            // Act
            var result = controller.CreateItem(new ItemDto());

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteItem_ReturnsNotFound_ForNoItem()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns((Item)null);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);

            // Act
            var result = controller.DeleteItem(testId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteItem_ReturnsNoContent_ForSuccessfulDelete()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns(new Item());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null, mapper: null);

            // Act
            var result = controller.DeleteItem(testId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
