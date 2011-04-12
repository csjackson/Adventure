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
    public class DropCommandTest
    {
           private IConsoleFacade console;
        private DropCommand cmd;
        private IRepository<GameObject> repository;
        private IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            cmd = new DropCommand(console, repository, player);
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
            var result = cmd.IsValid("drop ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_If_Item_Not_in_Inventory()
        {
            // arrange
            var db_Player = new GameObject() { GameObjectId = 3 };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring" };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            player.Stub(qq => qq.Id).Return(3); 

            // Act
            cmd.Execute("drop ring");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("'{0}' not in inventory.", "Ring"));
        }
        [TestMethod]
        public void Execute_Should_Change_Location_Of_Item_From_Inventory_to_Location_of_Player()
        {
        	 // arrange
            var db_Hallway = new GameObject() { GameObjectId = 8 };
            var db_Player = new GameObject() { GameObjectId = 3, Location= db_Hallway };
            var ball = new GameObject() { Name = "Ball", Location = db_Player };
            var ring = new GameObject() { Name = "Ring" };
            db_Player.Inventory.Add(ball);
            var list = new List<GameObject>() { db_Player, ball, ring };

            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            player.Stub(qq => qq.Id).Return(3); 

            // Act
            cmd.Execute("drop ball");
            // Assert
            Assert.AreEqual(ball.Location_Id, db_Hallway.GameObjectId);
            console.AssertWasCalled(qq => qq.WriteLine("You put down your {0}", "ball"));
        }
    }
}
