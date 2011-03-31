using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Commands;
using Adventure.Data;
namespace Adventure.Tests
{
    [TestClass]
    public class LookCommandTest
    {
        private IConsoleFacade mock;
        private LookCommand cmd;
        private IRepository<Room> roomRepository;
        private IRepository<Item> itemRepository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            roomRepository = MockRepository.GenerateMock<IRepository<Room>>();
            itemRepository = MockRepository.GenerateMock<IRepository<Item>>();
            cmd = new LookCommand(mock, roomRepository, itemRepository);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_for_Invalid_String()
        {
            // Arrange
            // Already created via TestInitialize

            // Act
            var result = cmd.IsValid("Stupid stuff");
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Look_Plus_Garbage_Should_Return_Quote_I_Dont_See_That_Here()
        {
            // Arrange
            // Already created via TestInitialize

            // Act
            var result = cmd.IsValid("look stupid");
            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("I don't see that here."));
        }

        [TestMethod]
        public void Look_and_RoomName_Should_Return_Desc_of_Room()
        {
            // Arrange
            var hallway = new Room() { RoomName = "Hallway", Description= "It's a hallway." };
            var bar = new Room() { RoomName = "Bar", Description = "I love this bar. It's my kinda of place." };
            var list = new List<Room>() { hallway, bar };
            roomRepository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());

            // Act
            cmd.Execute("look hallway");

            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("It's a hallway."));
            roomRepository.AssertWasCalled(m => m.Dispose());
        }
        [TestMethod]
        public void Look_AND_ItemName_Should_Return_ItemDesc()
        {
            // Arrange
            var ball = new Item() { ItemName= "Ball", ItemDescription= "A shiny rubber ball."};
            var sword = new Item() { ItemName = "Sword", ItemDescription = "Shiny and sharp." };
            var list = new List<Item>() { ball, sword };
            itemRepository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());

            // Act
            cmd.Execute("look sword");

            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("Shiny and sharp."));
            roomRepository.AssertWasCalled(m => m.Dispose());
        }
    }
}
