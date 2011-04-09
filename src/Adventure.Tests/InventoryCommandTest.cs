using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Data;
using Adventure.Commands;

namespace Adventure.Tests
{
    [TestClass]
    public class InventoryCommandTest
    {
        private IConsoleFacade console;
        private InventoryCommand cmd;
        private IRepository<GameObject> repository;
        private IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            cmd = new InventoryCommand(console, repository, player);
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
        public void isValid_Should_Return_True_for_Valid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("inventory");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_List_Contents_Of_Inventory()
        {
            // Arrange
            var db_Player = new GameObject() { GameObjectId = 3 };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring" };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            player.Stub(qq => qq.Id).Return(3); 
                
            // Act
            cmd.Execute("inventory");

            // Assert
            console.AssertWasCalled(m => m.Write("{0}  ", "Ball"));
            console.AssertWasNotCalled(m => m.Write("{0}  ", "Ring"));
            repository.AssertWasCalled(m => m.Dispose());
        }

    }
}
