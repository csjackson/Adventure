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
    public class GetCommandTest
    {
        private IConsoleFacade console;
        private GetCommand cmd;
        private IRepository<GameObject> repository;
        private IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            cmd = new GetCommand(console, repository, player);
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
            var result = cmd.IsValid("get ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_When_Item_Already_Held()
        {
            // Arrange
             var db_Player = new GameObject() { GameObjectId = 3 };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring" };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            // Act
            cmd.Execute("get ball");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("You already have that."));
        }
        [TestMethod]
        public void Execute_Should_Inform_When_Item_Not_In_Room_with_Player()
        {
            // Arrange
            var db_Hallway = new GameObject() { GameObjectId = 8 };
            var db_Elsewhere = new GameObject() { GameObjectId = 12 };
            var db_Player = new GameObject() { GameObjectId = 3, Location = db_Hallway };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring", Location = db_Elsewhere };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            // Act
            cmd.Execute("get ring");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("I do not see that, here."));
        }

        [TestMethod]
        public void Execute_Should_Add_Item_To_Inventory()
        {
            // Arrange
            var db_Hallway = new GameObject() { GameObjectId = 8 };
            var db_Elsewhere = new GameObject() { GameObjectId = 12 };
            var db_Player = new GameObject() { GameObjectId = 3, Location = db_Hallway };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring", Location = db_Hallway };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            // Act
            cmd.Execute("get ring");
            // Assert
            Assert.AreEqual(ring.Location, db_Player);
            console.AssertWasCalled(qq => qq.WriteLine("You pick up {0}", "ring"));
        }
    }
}
