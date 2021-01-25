using System;
using System.Collections.Generic;
using System.Linq;
using BillingApi.Controllers;
using BillingApi.Data;
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
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.GetItem(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetItem_ReturnsOk_ForFoundItem()
        {
            // Arrange
            int testId = 1;
            Item testItem = new Item(){Id=testId, Name="Shoes", Price=100, Discount=10};
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(testId)).Returns(testItem);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.GetItem(testId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var item = Assert.IsType<Item>(okResult.Value);
            Assert.Equal(testId, item.Id);
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
            mock.Setup(repo => repo.GetAllItems()).Returns(items);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.GetItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resItems = Assert.IsType<List<Item>>(okResult.Value);
            Assert.Equal(items.Count(), resItems.Count());
        }

        [Fact]
        public void GetItems_ReturnsNotFound_ForNoItems()
        {
            // Arrange
            List<Item> items = new List<Item>();
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetAllItems()).Returns(items);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.GetItems();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateItem_ReturnsCreate_ForValidItem()
        {
            // Arrange
            int testId = 1;
            Item testItem = new Item() {Name="NewItem", Manufacturer="USA", Price=50, Discount=0};
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.AddItem(testItem)).Returns(testId);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);
            
            // Act
            var result = controller.CreateItem(testItem);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(testId, createdResult.Value);
        }

        [Fact]
        public void CreateItem_ReturnsBadRequest_ForInvalidItem()
        {
            // Arrange
            var mock = new Mock<IItemRepository>();
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.CreateItem(item: null);

            // Assign
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void CreateItem_ReturnsUnprocessable_ForRepeatedItem()
        {
            // Assign
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemByName(It.IsAny<string>())).Returns(new Item());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.CreateItem(new Item());

            // Assert
            Assert.IsType<UnprocessableEntityResult>(result.Result);
        }

        [Fact]
        public void DeleteItem_ReturnsNotFound_ForNoItem()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns((Item)null);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.DeleteItem(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteItem_ReturnsNoContent_ForSuccessfulDelete()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns(new Item());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);

            // Act
            var result = controller.DeleteItem(testId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PartialUpdateItem_ReturnsNotFound_ForNoItem()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns((Item)null);
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);
            
            // Act
            var result = controller.PartialUpdateItem(testId, new Item());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialUpdateItem_ReturnsNoContent_ForNoItem()
        {
            // Arrange
            int testId = 5;
            var mock = new Mock<IItemRepository>();
            mock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns(new Item());
            var controller = new ItemsController(itemRepository: mock.Object, cartService: null);
            
            // Act
            var result = controller.PartialUpdateItem(testId, new Item());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
